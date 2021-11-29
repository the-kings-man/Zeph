using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zeph.Unity {
    public class GameController : MonoBehaviour {
        void Awake() {
            Zeph.Core.SystemLocator.CombatSystem = new Zeph.Core.Combat.CombatSystem();
            Zeph.Core.SystemLocator.InventorySystem = new Zeph.Core.Inventory.InventorySystem();
        }
    }
}
