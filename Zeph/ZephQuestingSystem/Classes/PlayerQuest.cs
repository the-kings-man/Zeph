using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.QuestingSystem.Classes {
    public class PlayerQuest : Zeph.Core.Classes.ClassBase<PlayerQuest> {
        public Guid pq_GUID = Guid.Empty;
        public Player pq_Player = null;
        public Quest pq_Quest = null;
        public Enums.PlayerQuestStatus pq_Status = Enums.PlayerQuestStatus.InProgress;
        public DateTime pq_Started = new DateTime(1900, 1, 1);
        public DateTime pq_Finished = new DateTime(1900, 1, 1);

        public List<PlayerQuestObjective> playerQuestObjectives = null;

        public PlayerQuest(Player p, Quest q, List<QuestObjective> questObjectives) {
            pq_GUID = new Guid();
            pq_Player = p;
            pq_Quest = q;
            pq_Status = Enums.PlayerQuestStatus.InProgress;
            pq_Started = Zeph.Core.GeneralOps.Now;

            playerQuestObjectives = new List<PlayerQuestObjective>();
            foreach (var qo in questObjectives) {
                playerQuestObjectives.Add(new PlayerQuestObjective(this, qo));
            }
        }

        public bool IsQuestComplete() {
            //TODO: Loop through PlayerQuestObjectives, if all their status is Complete, then we're finished.
            throw new NotImplementedException();
        }
    }
}
