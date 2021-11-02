using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Enums {
    public enum ItemType {
        None = -1,
        Armour = 0,
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// This was separated from Armour as it would be easier to distinguish when equipping and for the broad range of items.
        /// </remarks>
        Weapon = 1,
        Consumable = 2,
        Material = 3,
        Quest = 4
    }
}
