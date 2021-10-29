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

        private PlayerQuestObjective() {
            pqo_ID = 0;
        }

        public PlayerQuestObjective(PlayerQuest pq, QuestObjective qo) {
            pqo_ID = -1;
            pqo_PlayerQuest = pq.pq_ID;
            pqo_QuestObjective = qo.qo_ID;
        }

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static PlayerQuestObjective Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<PlayerQuestObjective> Read() {
            var lst = new List<PlayerQuestObjective>();
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var _lst = db.Read(TABLE);
                foreach (var dic in _lst) {
                    var _dic = ReadFromDictionary(dic);
                    if (_dic != null) {
                        lst.Add(_dic);
                    }
                }
            }
            return lst;
        }

        public new static PlayerQuestObjective ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var obj = new PlayerQuestObjective();
                obj.pqo_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                obj.pqo_PlayerQuest = GeneralOps.ConvertDatabaseField<int>(dic, "pqo_PlayerQuest");
                obj.pqo_QuestObjective = GeneralOps.ConvertDatabaseField<int>(dic, "pqo_QuestObjective");
                obj.pqo_Progress = GeneralOps.ConvertDatabaseField<int>(dic, "pqo_Progress");
                obj.pqo_Status = (Enums.PlayerQuestObjectiveStatus)GeneralOps.ConvertDatabaseField<int>(dic, "pqo_Status");
                return obj;
            } else {
                return null;
            }
        }

        public new static PlayerQuestObjective Save(PlayerQuestObjective obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.pqo_ID == -1) obj.pqo_ID = db.GetNextId(TABLE);
                dic["id"] = obj.pqo_ID;
                dic["pqo_PlayerQuest"] = obj.pqo_PlayerQuest;
                dic["pqo_QuestObjective"] = obj.pqo_QuestObjective;
                dic["pqo_Progress"] = obj.pqo_Progress;
                dic["pqo_Status"] = (int)obj.pqo_Status;
                return ReadFromDictionary(db.Save(TABLE, obj.pqo_ID, dic));
            }
        }

        #endregion
    }
}
