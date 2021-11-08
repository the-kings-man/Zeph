using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Inventory {
    public interface IInventorySystem {
        void Initialise();
        bool EquipBag(Classes.Player player, Classes.Items.Bag bag);
        ItemAddedResult AddItem(Classes.Player player, Classes.Item item, int quantity = 1);
        bool RemoveItem(Classes.PlayerBagSlot playerBagSlot, int quantity = -1);
        int GetQuantity(Classes.Player player, Classes.Item item);
    }

    public class InventorySystem : IInventorySystem {

        #region Event Handling
        public delegate void ItemAddedEventHandler(object sender, ItemAddedEventArgs e);
        public static event ItemAddedEventHandler OnItemAdded;

        public delegate void ItemRemovedEventHandler(object sender, ItemRemovedEventArgs e);
        public static event ItemRemovedEventHandler OnItemRemoved;

        public delegate void BagEquippedEventHandler(object sender, BagEquippedEventArgs e);
        public static event BagEquippedEventHandler BagEquipped;
        #endregion

        public void Initialise() {
        }

        /// <summary>
        /// Adds a bag to the players inventory.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="bag"></param>
        public bool EquipBag(Classes.Player player, Classes.Items.Bag bag) {
            bool bagAdded = false;


            if (bag.i_Type == Enums.ItemType.Consumable && bag.i_SubType == Enums.ItemSubType.Bag) {
                var playerBag = new Classes.PlayerBag(player.p_ID, bag.i_ID, bag.Slots);
                for (var i = 0; i < playerBag.pb_Slots; i++) {
                    var playerBagSlot = new Classes.PlayerBagSlot(playerBag.pb_ID);
                    playerBag.PlayerBagSlots.Add(playerBagSlot);
                }
                Classes.PlayerBag.Save(playerBag);
                player.PlayerBags.Add(playerBag);

                BagEquipped?.Invoke(null, new BagEquippedEventArgs() {
                    Player = player,
                    Bag = bag,
                    PlayerBag = playerBag,
                });

                bagAdded = true;
            } else {
                throw new Classes.ExceptionHandling.GeneralException("InventorySystem", 1, "Could not add bag as item " + bag.i_ID + " is not a bag.");
            }

            return bagAdded;
        }

        public ItemAddedResult AddItem(Classes.Player player, Classes.Item item, int quantity = 1) {
            if (quantity > item.i_MaxQuantity) {
                //This logic could be changed to just split the stack of items that's coming in. But this works for now too
                throw new Classes.ExceptionHandling.GeneralException("InventorySystem", 2, "quantity (" + quantity.ToString() + ") is greater than the items ( " + item.i_ID.ToString() + ") max quantity " + item.i_MaxQuantity.ToString());
            } else if (quantity < 1) {
                throw new Classes.ExceptionHandling.GeneralException("InventorySystem", 3, "quantity (" + quantity.ToString() + ") cannot be less than 1");
            }

            var res = new ItemAddedResult();
            var args = new ItemAddedEventArgs() {
                Player = player,
                Item = item,
                Quantity = quantity
            };

            foreach (var pb in player.PlayerBags) {
                foreach (var pbs in pb.PlayerBagSlots) {
                    if (pbs.pbs_Item == -1) {
                        pbs.pbs_Item = item.i_ID;
                        pbs.pbs_Quantity = quantity;
                        Classes.PlayerBagSlot.Save(pbs);

                        res.playerBagSlots.Add(pbs);
                        res.itemAdded = true;

                        args.PlayerBagSlots.Add(pbs);
                    } else {
                        if (pbs.pbs_Item == item.i_ID) {
                            if (quantity + pbs.pbs_Quantity <= item.i_MaxQuantity) {
                                pbs.pbs_Quantity += quantity;
                                Classes.PlayerBagSlot.Save(pbs);

                                res.playerBagSlots.Add(pbs);
                                res.itemAdded = true;

                                args.PlayerBagSlots.Add(pbs);
                            }
                        }
                    }

                    if (res.itemAdded) break;
                }

                if (res.itemAdded) break;
            }

            if (res.itemAdded) {
                OnItemAdded?.Invoke(null, args);
            }

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playerBagSlot"></param>
        /// <param name="quantity">-1 to remove all, else just the quantity specified. If the quantity is greater than the slot quantity, will just remove all</param>
        /// <returns></returns>
        public bool RemoveItem(Classes.PlayerBagSlot playerBagSlot, int quantity = -1) {
            Classes.Item item = playerBagSlot.pbs_Item == -1 ? null : new Classes.Item() { i_ID = playerBagSlot.pbs_Item };
            Classes.Player player = new Classes.Player() { p_ID = (Classes.PlayerBag.Read(playerBagSlot.pbs_PlayerBag).pb_Player) };
            int pbsQuantity = playerBagSlot.pbs_Quantity;

            if (quantity == -1) {
                playerBagSlot.pbs_Item = -1;
                playerBagSlot.pbs_Quantity = 0;
            } else {
                playerBagSlot.pbs_Quantity -= quantity;
                if (playerBagSlot.pbs_Quantity < 1) {
                    playerBagSlot.pbs_Item = -1;
                    playerBagSlot.pbs_Quantity = 0;
                } else {
                    playerBagSlot.pbs_Quantity -= quantity;
                }
            }
            Classes.PlayerBagSlot.Save(playerBagSlot);

            OnItemRemoved?.Invoke(null, new ItemRemovedEventArgs() {
                Player = player,
                Item = item,
                PlayerBagSlot = playerBagSlot,
                Quantity = pbsQuantity
            });

            return true;
        }

        /// <summary>
        /// Get the total quantity of the item in the players inventory
        /// </summary>
        /// <param name="player"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetQuantity(Classes.Player player, Classes.Item item) {
            int quantity = 0;
            foreach (var pb in player.PlayerBags) {
                foreach (var pbs in pb.PlayerBagSlots) {
                    if (pbs.pbs_Item == item.i_ID) {
                        quantity += pbs.pbs_Quantity;
                    }
                }
            }
            return quantity;
        }
    }

    public class ItemAddedResult {
        public bool itemAdded = false;
        public List<Classes.PlayerBagSlot> playerBagSlots = new List<Classes.PlayerBagSlot>();
    }

    public class ItemAddedEventArgs : EventArgs {
        public Classes.Player Player { get; set; }
        public Classes.Item Item { get; set; }
        public List<Classes.PlayerBagSlot> PlayerBagSlots { get; set; } = new List<Classes.PlayerBagSlot>();
        public int Quantity { get; set; }
    }

    public class ItemRemovedEventArgs : EventArgs {
        public Classes.Player Player { get; set; }
        public Classes.Item Item { get; set; }
        public Classes.PlayerBagSlot PlayerBagSlot { get; set; }
        public int Quantity { get; set; }
    }

    public class BagEquippedEventArgs : EventArgs {
        public Classes.Player Player { get; set; }
        public Classes.Items.Bag Bag { get; set; }
        public Classes.PlayerBag PlayerBag { get; set; }
    }
}
