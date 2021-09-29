using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    [Serializable]
    public class PlayerQuestObjective : Zeph.Core.Classes.ClassBase<PlayerQuestObjective> {
        const string TABLE = "playerQuestObjective";

        public int pqo_ID = -1;
        public int pqo_PlayerQuest = -1;
        public int pqo_QuestObjective = -1;
        public int pqo_Progress = 0;
        public Enums.PlayerQuestObjectiveStatus pqo_Status = Enums.PlayerQuestObjectiveStatus.InProgress;

        public PlayerQuestObjective(PlayerQuest pq, QuestObjective qo) {
            pqo_ID = -1;
            pqo_PlayerQuest = pq.pq_ID;
            pqo_QuestObjective = qo.qo_ID;
        }
    }
}
