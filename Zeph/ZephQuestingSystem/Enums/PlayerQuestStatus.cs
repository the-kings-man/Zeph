using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.QuestingSystem.Enums {
    public enum PlayerQuestStatus {
        /// <summary>
        /// Quest is currently being undertaken by the player
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// Quest has been completed by the player
        /// </summary>
        Complete = 2,
        /// <summary>
        /// Quest is not currently being undertaken by the player, they discarded it or something
        /// </summary>
        Inactive = 3
    }
}
