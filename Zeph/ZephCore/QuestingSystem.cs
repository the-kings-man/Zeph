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
        public static void Initialise() {
            InventorySystem.ItemAdded += (s, e) => {
                ItemProgress(e.Player, e.Item);
            };

            InventorySystem.ItemRemoved += (s, e) => {
                ItemProgress(e.Player, e.Item);
            };

            ZoneSystem.ZoneEntered += (s, e) => {
                if (e.Trigger != null) {
                    TriggerProgress(e.Player, e.Trigger, 1);
                }
            };
        }

        public static Classes.PlayerQuest Start(Zeph.Core.Classes.Player player, Classes.Quest quest) {
            var pq = new Classes.PlayerQuest(player, quest, quest.QuestObjectives);
            return Classes.PlayerQuest.Save(pq);
        }

        public static Classes.PlayerQuest HandIn(Zeph.Core.Classes.Player player, Classes.Quest quest) {
            foreach (var pq in Zeph.Core.Classes.PlayerQuest.Read()) {
                if (pq.pq_Player.p_ID == player.p_ID && pq.pq_Quest.q_ID == quest.q_ID) {
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

        public static QuestProgressResult TriggerProgress(Zeph.Core.Classes.Player player, Classes.Trigger trigger, int progress) {
            return logProgressOnQuest(player, (qo, pqo) => {
                if (qo.qo_Type == Enums.QuestObjectiveType.Trigger && qo.qo_Trigger == trigger.t_ID) {
                    //Progress! woo
                    pqo.pqo_Progress += progress;

                    return true;
                } else {
                    return false;
                }
            });
        }

        public static QuestProgressResult ItemProgress(Zeph.Core.Classes.Player player, Classes.Item item) {
            return logProgressOnQuest(player, (qo, pqo) => {
                if (qo.qo_Type == Enums.QuestObjectiveType.Gather && qo.qo_Item == item.i_ID) {
                    //get the total quantity of this item the player has in inventory in total
                    var quantityInInventory = SystemLocator.GetService<IInventorySystem>().GetQuantity(player, item);

                    bool wasProgressLogged = false;
                    if (quantityInInventory > qo.qo_Goal) {
                        pqo.pqo_Progress = qo.qo_Goal;
                        wasProgressLogged = true;
                    } else {
                        if (pqo.pqo_Progress != quantityInInventory) {
                            wasProgressLogged = true;
                            pqo.pqo_Progress = quantityInInventory;
                        }
                    }

                    return wasProgressLogged;
                } else {
                    return false;
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="fn">The function which gets called to perform the progress on the <see cref="Classes.PlayerQuestObjective"/> object. Return true if actual progress was logged towards the players quest.</param>
        /// <returns></returns>
        private static QuestProgressResult logProgressOnQuest(Zeph.Core.Classes.Player player, Func<Classes.QuestObjective, Classes.PlayerQuestObjective, bool> fn) {
            var res = new QuestProgressResult();

            //Get all quests for player who have current objectives of type item.
            // If quest bojective item is this item, work out how many of this item the player has in inventory in total, log progress

            var playersCurrentPlayerQuests = Classes.PlayerQuest.Read();
            foreach (var pq in playersCurrentPlayerQuests) {
                if (pq.pq_Finished.Year == 1900 && //not finished
                    pq.pq_Player.p_ID == player.p_ID) {

                    bool anObjectiveWasFinished = false;
                    foreach (var pqo in pq.PlayerQuestObjectives) {
                        var qo = Classes.QuestObjective.Read(pqo.pqo_QuestObjective);
                        if (pqo.pqo_Progress != qo.qo_Goal) { //is not finished
                            if (fn(qo, pqo)) {
                                //Progress! woo
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
