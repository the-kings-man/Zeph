using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.QuestingSystem.Enums {
    public enum QuestObjectivesType {
        /// <summary>
        /// The objectives within this quest can be completed in any order
        /// </summary>
        Generic = 1,
        /// <summary>
        /// The objectsives within this quest must be completed in procedural order
        /// </summary>
        Procedural = 2
    }
}
