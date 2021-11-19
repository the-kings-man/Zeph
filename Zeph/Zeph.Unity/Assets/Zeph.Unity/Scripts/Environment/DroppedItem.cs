using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ZephGame {
    public class DroppedItem : MonoBehaviour {
        public int id;
        public int quantity;

        public void Awake() {
            if (id <= 0) {
                throw new Exception("DroppedItem must have an id.");
            }
        }

        public void PickUp() {
            //Pick up the object in the inventory
            //Alert the questing system also
            var inv = Zeph.Core.SystemLocator.GetService<Zeph.Core.Inventory.IInventorySystem>();

            var res = inv.AddItem(GeneralOps.CurrentPlayer, Zeph.Core.Classes.Item.Read(id), quantity);
            Debug.Log("Item " + id.ToString() + ", " + inv.GetQuantity(GeneralOps.CurrentPlayer, Zeph.Core.Classes.Item.Read(id)) + " quantity");
            if (res.itemAdded) {
                Destroy(this.gameObject);
            } else {
                Debug.Log("Item " + id.ToString() + " could not be picked up.");
            }
        }
    }
}
