using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Enums {
    public enum AttackType {
        None = 0,
        /// <summary>
        /// An instant attack inflicted instantly upon the target
        /// </summary>
        Instant = 1,
        /// <summary>
        /// A projectile strike, like an arrow from a bow and arrow or a fireball spell
        /// </summary>
        Projectile = 2
    }
}
