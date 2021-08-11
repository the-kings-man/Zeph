using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.QuestingSystem.Classes {
    [Serializable]
    public class PlayerQuest : Zeph.Core.Classes.IClass {
        public Guid pq_GUID = Guid.Empty;
        public Guid pq_Player = Guid.Empty;
        public Guid pq_Quest = Guid.Empty;
        public Enums.PlayerQuestStatus pq_Status = Enums.PlayerQuestStatus.InProgress;
        public DateTime pq_Started = new DateTime(1900, 1, 1);
        public DateTime pq_Finished = new DateTime(1900, 1, 1);

        public bool IsQuestComplete() {
            //TODO: Loop through PlayerQuestObjectives, if all their status is Complete, then we're finished.
            throw new NotImplementedException();
        }
    }
}
