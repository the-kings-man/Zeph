﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public class QuestingSystem {
        //public static bool ObjectiveProgressed(Classes.PlayerQuestObjective pqo, int progress) {
        //    //Progress the playerQuestObjective object
        //    pqo.pqo_Progress += progress;

        //    //Get the quest objective
        //    var qo = new Classes.QuestObjective(pqo.pqo_QuestObjective);

        //    if (qo.qo_Goal <= pqo.pqo_Progress) {
        //        //Quest finished
        //        pqo.pqo_Status = Enums.PlayerQuestObjectiveStatus.Complete;

        //        var playerQuest = new Classes.PlayerQuest(pqo.pqo_PlayerQuest);
        //        if (playerQuest.IsQuestComplete()) {
        //            playerQuest.pq_Status = Enums.PlayerQuestStatus.Complete;
        //        }
        //    }
        //}

        public static Classes.PlayerQuest Start(Zeph.Core.Classes.Player p, Classes.Quest q) {
            var pq = new Classes.PlayerQuest(p, q, q.questObjectives);
            Classes.PlayerQuest.Save(pq);
            return pq;
        }

    }
}
