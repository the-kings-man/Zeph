using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    /// <summary>
    /// A class to allow the game application to pick and choose which systems they use for the different elements of their games.
    /// </summary>
    /// <remarks>
    /// For example, if the user wanted to use a different type of inventory system, they could code their own using <see cref="Inventory.IInventorySystem"/>
    /// and then provide it through <see cref="SystemLocator.InventorySystem"/>. Then whenever the RPG framework uses <see cref="GetService{Inventory.IInventorySystem}"/>
    /// it will return them their custom service. And throughout the framework, whenever the inventory is utilised, the relevant system will also
    /// use their custom inventory system (e.g. <see cref="Questing.QuestingSystem"/> whenever an item is added to progress a quest).
    /// </remarks>
    public class SystemLocator {

        private static Inventory.IInventorySystem _inventorySystem = null;
        public static Inventory.IInventorySystem @InventorySystem {
            set {
                _inventorySystem = value;
            }
        }

        private static Combat.ICombatSystem _combatSystem = null;
        public static Combat.ICombatSystem @CombatSystem {
            set {
                _combatSystem = value;
            }
        }

        public static T GetService<T>() {
            if (typeof(T) == typeof(Inventory.IInventorySystem)) {
                return (T)_inventorySystem;
            } else if (typeof(T) == typeof(Combat.ICombatSystem)) {
                return (T)_combatSystem;
            }
            return default(T);
        }
    }
}
