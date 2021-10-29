using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    [Serializable]
    public class Quest : Zeph.Core.Classes.ClassBase<Quest> {
        const string TABLE = "quest";

        /// <summary>
        /// The ID of the quest
        /// </summary>
        public int q_ID = -1;
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

        private List<QuestObjective> questObjectives = null;

        #region Properties

        public List<QuestObjective> QuestObjectives {
            get {

                if (questObjectives == null) {
                    questObjectives = new List<QuestObjective>();
                    var lstQuestObjectives = QuestObjective.Read();
                    foreach (var qo in lstQuestObjectives) {
                        if (qo.qo_Quest == q_ID) {
                            questObjectives.Add(qo);
                        }
                    }
                }
                return questObjectives;
            }
        }

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Quest Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Quest> Read() {
            var lst = new List<Quest>();
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

        public new static Quest ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Quest();
                    obj.q_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.q_Name = GeneralOps.ConvertDatabaseField<string>(dic, "q_Name");
                    obj.q_Description = GeneralOps.ConvertDatabaseField<string>(dic, "q_Description");
                    obj.q_ObjectivesType = (Enums.QuestObjectivesType)GeneralOps.ConvertDatabaseField<int>(dic, "q_ObjectivesType");
                    obj.q_ReceivalType = (Enums.QuestReceivalType)GeneralOps.ConvertDatabaseField<int>(dic, "q_ReceivalType");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Quest", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Quest Save(Quest obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.q_ID == -1) obj.q_ID = db.GetNextId(TABLE);
                dic["id"] = obj.q_ID;
                dic["q_Name"] = obj.q_Name;
                dic["q_Description"] = obj.q_Description;
                dic["q_ObjectivesType"] = (int)obj.q_ObjectivesType;
                dic["q_ReceivalType"] = (int)obj.q_ReceivalType;
                return ReadFromDictionary(db.Save(TABLE, obj.q_ID, dic));
            }
        }

        #endregion
    }
}
