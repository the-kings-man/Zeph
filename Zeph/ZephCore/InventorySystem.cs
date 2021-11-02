using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public class InventorySystem {
        /// <summary>
        /// Adds a bag to the players inventory.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="bag"></param>
        public bool AddBag(Classes.Player player, Classes.Items.Bag bag) {
            bool bagAdded = false;

            if (bag.i_Type == Enums.ItemType.Consumable && bag.i_SubType == Enums.ItemSubType.Bag) {
                var playerBag = new Classes.PlayerBag(player.p_ID, bag.i_ID, bag.Slots);
                for (var i = 0; i < playerBag.pb_Slots; i++) {
                    var playerBagSlot = new Classes.PlayerBagSlot(playerBag.pb_ID);
                    playerBag.PlayerBagSlots.Add(playerBagSlot);
                }
                Classes.PlayerBag.Save(playerBag);
                player.PlayerBags.Add(playerBag);

                bagAdded = true;
            } else {
                throw new Classes.ExceptionHandling.GeneralException("InventorySystem", 1, "Could not add bag as item " + bag.i_ID + " is not a bag.");
            }

            return false;
        }

        public ItemAddedResult AddItem(Classes.Player player, Classes.Item item, int quantity = 1) {
            if (quantity > item.i_MaxQuantity) {
                //This logic could be changed to just split the stack of items that's coming in. But this works for now too
                throw new Classes.ExceptionHandling.GeneralException("InventorySystem", 2, "quantity (" + quantity.ToString() + ") is greater than the items ( " + item.i_ID.ToString() + ") max quantity " + item.i_MaxQuantity.ToString());
            } else if (quantity < 1) {
                throw new Classes.ExceptionHandling.GeneralException("InventorySystem", 3, "quantity (" + quantity.ToString() + ") cannot be less than 1");
            }

            var res = new ItemAddedResult();

            foreach (var pb in player.PlayerBags) {
                foreach (var pbs in pb.PlayerBagSlots) {
                    if (pbs.pbs_Item == -1) {
                        pbs.pbs_Item = item.i_ID;
                        pbs.pbs_Quantity = quantity;
                        Classes.PlayerBagSlot.Save(pbs);

                        res.playerBagSlots.Add(pbs);
                        res.itemAdded = true;
                    } else {
                        if (pbs.pbs_Item == item.i_ID) {
                            if (quantity + pbs.pbs_Quantity <= item.i_MaxQuantity) {
                                pbs.pbs_Quantity += quantity;
                                Classes.PlayerBagSlot.Save(pbs);

                                res.playerBagSlots.Add(pbs);
                                res.itemAdded = true;
                            }
                        }
                    }

                    if (res.itemAdded) break;
                }

                if (res.itemAdded) break;
            }

            //if (res.itemAdded) {
            //    QuestingSystem.TriggerItemAdded(item.i_ID, quantity);
            //}

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

            //QuestingSystem.TriggerItemRemoved(playerBagSlot.pbs_Item, quantity);

            return true;
        }
    }

    public class ItemAddedResult {
        public bool itemAdded = false;
        public List<Classes.PlayerBagSlot> playerBagSlots = new List<Classes.PlayerBagSlot>();
    }
}
