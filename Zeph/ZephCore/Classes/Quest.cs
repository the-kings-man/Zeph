using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    [Serializable]
    public class Quest : Zeph.Core.Classes.ClassBase {
        /// <summary>
        /// The GUID of the quest
        /// </summary>
        public Guid q_GUID = Guid.Empty;
        /// <summary>
        /// Name of the quest
        /// </summary>
        public string q_Name = "";
        /// <summary>
        /// Description of the quest
        /// </summary>
        public string q_Description = "";
        /// <summary>
        /// The type of quest, i.e. to be completed in any order, or procedural one objective after another
        /// </summary>
        public Enums.QuestObjectivesType q_ObjectivesType = Enums.QuestObjectivesType.Generic;
        /// <summary>
        /// How often this quest can be received, either once off or recurring. Will be used mainly for daily/weekly quests where the quest giver will run code to see whether or not to offer the quest
        /// </summary>
        public Enums.QuestReceivalType q_ReceivalType = Enums.QuestReceivalType.OnceOff;

        public static PlayerQuest Start(Zeph.Core.Classes.Player p, Classes.Quest q) {
            var pq = new Classes.PlayerQuest(p, q, q.questObjectives);
            PlayerQuest.Save(pq);
            return pq;
        }
    }
}
