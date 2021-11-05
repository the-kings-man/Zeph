using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public class SystemLocator {

        private static IInventorySystem _inventorySystem = null;
        public static IInventorySystem @InventorySystem {
            set {
                _inventorySystem = value;
            }
        }

        public static T GetService<T>() {
            if (typeof(T) is IInventorySystem) {
                return (T)_inventorySystem;
            }
            return default(T);
        }
    }
}
