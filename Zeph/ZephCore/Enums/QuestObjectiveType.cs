using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Enums {
    public enum QuestObjectiveType {
        /// <summary>
        /// Defeat an enemy
        /// </summary>
        Defeat = 1,
        /// <summary>
        /// Gather some materials
        /// </summary>
        Gather = 2,
        /// <summary>
        /// Trigger a... trigger...? (i.e. walk over a bridge which has an invisible trigger)
        /// </summary>
        Trigger = 3
    }
}
