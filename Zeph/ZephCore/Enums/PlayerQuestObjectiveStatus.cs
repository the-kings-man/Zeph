using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Enums {
    public enum PlayerQuestObjectiveStatus {
        /// <summary>
        /// Objective is currently being undertaken by the player
        /// </summary>
        InProgress = 1,
        /// <summary>
        /// Objective has been completed by the player
        /// </summary>
        Complete = 2,
        /// <summary>
        /// Objective has not yet been started, i.e. a previous objective is underway as this objects <see cref="Classes.Quest"/> <see cref="Classes.Quest.q_ObjectivesType"/> could be <see cref="Enums.QuestObjectivesType.Procedural"/>
        /// </summary>
        NotStarted = 3
    }
}
