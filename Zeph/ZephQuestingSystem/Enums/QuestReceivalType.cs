using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.QuestingSystem.Enums {
    public enum QuestReceivalType {
        /// <summary>
        /// A quest which can only be received once. It can not be repeated.
        /// </summary>
        OnceOff = 1,
        /// <summary>
        /// The quest can be received more than once, e.g. Daily quests
        /// </summary>
        Recurring = 2
    }
}
