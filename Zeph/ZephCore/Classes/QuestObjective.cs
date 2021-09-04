using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    [Serializable]
    public class QuestObjective : Zeph.Core.Classes.ClassBase {
        /// <summary>
        /// The GUID of the QuestObjective
        /// </summary>
        public Guid qo_GUID = Guid.Empty;
        /// <summary>
        /// The <see cref="Quest.q_GUID"/> of the <see cref="Quest"/> this QuestObjective is linked to
        /// </summary>
        public Guid qo_Quest = Guid.Empty;
        /// <summary>
        /// Objective description i.e. "Gather 5 apples", "Defeat 5 ravenous rabbits"
        /// </summary>
        public string qo_Description = "";
        /// <summary>
        /// The purpose of the objective, is it to defeat an enemy? Or gather some materials?
        /// </summary>
        private Enums.QuestObjectiveType qo_Type = Enums.QuestObjectiveType.Defeat;
        /// <summary>
        /// How many of the objective must be defeated/gathered/triggered before the objective is fulfilled.
        /// </summary>
        public int qo_Goal = 0;
        /// <summary>
        /// The order this quest objective sits within the quest. For <see cref="Enums.QuestObjectivesType.Procedural"/> quests, this is the order the objective MUST be completed.
        /// </summary>
        public int qo_Order = 0;
    }
}
