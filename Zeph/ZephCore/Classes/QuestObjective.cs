using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    [Serializable]
    public class QuestObjective : Zeph.Core.Classes.ClassBase<QuestObjective> {
        const string TABLE = "questObjective";

        /// <summary>
        /// The ID of the QuestObjective
        /// </summary>
        public int qo_ID = -1;
        /// <summary>
        /// The <see cref="Quest.q_ID"/> of the <see cref="Quest"/> this QuestObjective is linked to
        /// </summary>
        public int qo_Quest = -1;
        /// <summary>
        /// Objective description i.e. "Gather 5 apples", "Defeat 5 ravenous rabbits"
        /// </summary>
        public string qo_Description = "";
        /// <summary>
        /// The purpose of the objective, is it to defeat an enemy? Or gather some materials?
        /// </summary>
        public Enums.QuestObjectiveType qo_Type = Enums.QuestObjectiveType.Defeat;
        /// <summary>
        /// How many of the objective must be defeated/gathered/triggered before the objective is fulfilled.
        /// </summary>
        public int qo_Goal = 0;
        /// <summary>
        /// The order this quest objective sits within the quest. For <see cref="Enums.QuestType.Procedural"/> quests, this is the order the objective MUST be completed.
        /// </summary>
        public int qo_Order = 0;
        /// <summary>
        /// The id of the trigger for this quest objective. The user triggering this trigger counts as progress towards this objective. Used when <see cref="qo_Type"/> = <see cref="Enums.QuestObjectiveType.Trigger"/>
        /// </summary>
        public int qo_Trigger = -1;

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static QuestObjective Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<QuestObjective> Read() {
            var lst = new List<QuestObjective>();
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

        public new static QuestObjective ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new QuestObjective();
                    obj.qo_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.qo_Quest = GeneralOps.ConvertDatabaseField<int>(dic, "qo_Quest");
                    obj.qo_Description = GeneralOps.ConvertDatabaseField<string>(dic, "qo_Description");
                    obj.qo_Type = (Enums.QuestObjectiveType)GeneralOps.ConvertDatabaseField<int>(dic, "qo_Type");
                    obj.qo_Goal = GeneralOps.ConvertDatabaseField<int>(dic, "qo_Goal");
                    obj.qo_Order = GeneralOps.ConvertDatabaseField<int>(dic, "qo_Order");
                    obj.qo_Trigger = GeneralOps.ConvertDatabaseField<int>(dic, "qo_Trigger");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("QuestObjective", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static QuestObjective Save(QuestObjective obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.qo_ID == -1) obj.qo_ID = db.GetNextId(TABLE);
                dic["id"] = obj.qo_ID;
                dic["qo_Quest"] = obj.qo_Quest;
                dic["qo_Description"] = obj.qo_Description;
                dic["qo_Type"] = (int)obj.qo_Type;
                dic["qo_Goal"] = obj.qo_Goal;
                dic["qo_Order"] = obj.qo_Order;
                dic["qo_Trigger"] = obj.qo_Trigger;
                return ReadFromDictionary(db.Save(TABLE, obj.qo_ID, dic));
            }
        }

        #endregion
    }
}
