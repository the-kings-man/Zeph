using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.QuestingSystem.Classes {
    [Serializable]
    public class PlayerQuestObjective : Zeph.Core.Classes.ClassBase<PlayerQuestObjective> {
        public Guid pqo_GUID = Guid.Empty;
        public Guid pqo_PlayerQuest = Guid.Empty;
        public Guid pqo_QuestObjective = Guid.Empty;
        public int pqo_Progress = 0;
        public Enums.PlayerQuestObjectiveStatus pqo_Status = Enums.PlayerQuestObjectiveStatus.InProgress;

        public PlayerQuestObjective(PlayerQuest pq, QuestObjective qo) {
            pqo_GUID = Guid.NewGuid();
            pqo_PlayerQuest = pq;
            pqo_QuestObjective = qo;
        }
    }
}
