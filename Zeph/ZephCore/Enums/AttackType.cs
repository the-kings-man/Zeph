using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Enums {
    public enum AttackType {
        None = 0,
        /// <summary>
        /// A strike with a melee weapon
        /// </summary>
        Strike = 1,
        /// <summary>
        /// A projectile strike, like an arrow from a bow and arrow
        /// </summary>
        Projectile = 2,
        /// <summary>
        /// An attack cast from a spell
        /// </summary>
        Spell = 3
    }
}
