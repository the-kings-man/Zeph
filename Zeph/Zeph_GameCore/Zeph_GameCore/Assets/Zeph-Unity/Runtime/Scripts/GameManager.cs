using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Zeph.Unity {
    public class GameManager : MonoBehaviour {
        // Start is called before the first frame update
        void Awake() {
            Zeph.Core.SystemLocator.InventorySystem = new Zeph.Core.Inventory.InventorySystem();
            Zeph.Core.SystemLocator.CombatSystem = new Zeph.Core.Combat.CombatSystem();
            Zeph.Core.Questing.QuestingSystem.Initialise();

            GeneralOps.Initialise();
        }
    }
}