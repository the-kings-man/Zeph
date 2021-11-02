using System;
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
            var pq = new Classes.PlayerQuest(p, q, q.QuestObjectives);
            return Classes.PlayerQuest.Save(pq);
        }

        public static Classes.PlayerQuest HandIn(Zeph.Core.Classes.Player p, Classes.Quest q) {
            foreach (var pq in Zeph.Core.Classes.PlayerQuest.Read()) {
                if (pq.pq_Player.p_ID == p.p_ID && pq.pq_Quest.q_ID == q.q_ID) {
                    if (pq.pq_Finished.Year != 1900 && !pq.pq_HandedIn) {
                        //Can hand in
                        return HandIn(pq);
                    }
                }
            }
            return null;
        }

        public static Classes.PlayerQuest HandIn(Classes.PlayerQuest pq) {
            pq.pq_HandedIn = true;
            return Zeph.Core.Classes.PlayerQuest.Save(pq, false);
        }

        public static QuestProgressResult TriggerProgress(Zeph.Core.Classes.Player p, Classes.Trigger t, int progress) {
            /**
             * Trigger
             * id
             * t_Description
             * t_NPC
             * t_Area
             */

            /**
             * Area
             * id
             * t_Description
             * 
             */

            /**
             * QuestProgressResult
             * A class that helps the game handle the result of quest progress...?
             * i.e. Did this finish the quest?
             * Did this finish the quest objective?
             * Is threre a next quest objective? If so, what?
             */

            var res = new QuestProgressResult();

            //Get all quests for player who have current objectives of type trigger.
            // If quest bojective trigger is this trigger, log progress

            var playersCurrentPlayerQuests = Classes.PlayerQuest.Read();
            foreach (var pq in playersCurrentPlayerQuests) {
                if (pq.pq_Finished.Year == 1900 && //not finished
                    pq.pq_Player.p_ID == p.p_ID) {

                    bool anObjectiveWasFinished = false;
                    foreach (var pqo in pq.PlayerQuestObjectives) {
                        var qo = Classes.QuestObjective.Read(pqo.pqo_QuestObjective);
                        if (pqo.pqo_Progress != qo.qo_Goal) { //is not finished
                            if (qo.qo_Type == Enums.QuestObjectiveType.Trigger && qo.qo_Trigger == t.t_ID) {
                                //Progress! woo
                                pqo.pqo_Progress += progress;
                                var _pqo = Classes.PlayerQuestObjective.Save(pqo);

                                bool finishedObjective = _pqo.pqo_Progress == qo.qo_Goal;

                                if (finishedObjective) anObjectiveWasFinished = true;

                                res.progressed = true;
                                res.objectiveResults.Add(new QuestProgressObjectiveResult() {
                                    playerQuestObjective = _pqo,
                                    finished = finishedObjective
                                });
                            }
                        }
                    }

                    if (anObjectiveWasFinished) {
                        bool theresAnObjectiveUnfinished = false;
                        foreach (var pqo in pq.PlayerQuestObjectives) {
                            var qo = Classes.QuestObjective.Read(pqo.pqo_QuestObjective);
                            if (pqo.pqo_Progress != qo.qo_Goal) { //is not finished
                                theresAnObjectiveUnfinished = true;
                                break;
                            }
                        }

                        if (!theresAnObjectiveUnfinished) {
                            pq.pq_Finished = DateTime.Now;
                            var _pq = Classes.PlayerQuest.Save(pq);

                            res.questsFinished.Add(_pq);
                        }
                    }
                }
            }

            return res;
        }
    }

    public class QuestProgressObjectiveResult {
        public Classes.PlayerQuestObjective playerQuestObjective = null;
        public bool finished = false;
    }

    public class QuestProgressResult {
        public List<QuestProgressObjectiveResult> objectiveResults = new List<QuestProgressObjectiveResult>();
        public List<Classes.PlayerQuest> questsFinished = new List<Classes.PlayerQuest>();
        public bool progressed = false;
    }
}
