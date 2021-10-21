using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Classes {
    public class PlayerQuest : Zeph.Core.Classes.ClassBase<PlayerQuest> {
        const string TABLE = "playerQuest";

        public int pq_ID = -1;
        public Player pq_Player = null;
        public Quest pq_Quest = null;
        public Enums.PlayerQuestStatus pq_Status = Enums.PlayerQuestStatus.InProgress;
        public DateTime pq_Started = new DateTime(1900, 1, 1);
        public DateTime pq_Finished = new DateTime(1900, 1, 1);

        public List<PlayerQuestObjective> playerQuestObjectives = null;

        private PlayerQuest() {
        }

        public PlayerQuest(Player p, Quest q, List<QuestObjective> questObjectives) {
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

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static PlayerQuest Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<PlayerQuest> Read() {
            var lst = new List<PlayerQuest>();
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

        public new static PlayerQuest ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new PlayerQuest();
                    obj.pq_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.pq_Player = new Player() { p_ID = GeneralOps.ConvertDatabaseField<int>(dic, "pq_Player") };
                    obj.pq_Quest = new Quest() { q_ID = GeneralOps.ConvertDatabaseField<int>(dic, "pq_Quest") };
                    obj.pq_Status = (Enums.PlayerQuestStatus)GeneralOps.ConvertDatabaseField<int>(dic, "pq_Status");
                    obj.pq_Started = GeneralOps.ConvertDatabaseField<DateTime>(dic, "pq_Started");
                    obj.pq_Finished = GeneralOps.ConvertDatabaseField<DateTime>(dic, "pq_Finished");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("PlayerQuest", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static PlayerQuest Save(PlayerQuest obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.pq_ID == -1) obj.pq_ID = db.GetNextId(TABLE);
                dic["id"] = obj.pq_ID;
                dic["pq_Player"] = obj.pq_Player.p_ID;
                dic["pq_Quest"] = obj.pq_Quest.q_ID;
                dic["pq_Status"] = (int)obj.pq_Status;
                dic["pq_Started"] = obj.pq_Started;
                dic["pq_Finished"] = obj.pq_Finished;
                return ReadFromDictionary(db.Save(TABLE, obj.pq_ID, dic));
            }
        }

        #endregion
    }
}
