using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    /// <summary>
    /// The object to be used for buffs/debuffs or damage/healing over time
    /// </summary>
    public class StatModifier : Zeph.Core.Classes.ClassBase<StatModifier> {
        const string TABLE = "statModifier";

        /// <summary>
        /// The ID of the statModifier
        /// </summary>
        public int sm_ID = -1;
        /// <summary>
        /// The name of this statModifier
        /// </summary>
        public string sm_Name = "";
        /// <summary>
        /// The description of this statModifier
        /// </summary>
        public string sm_Description = "";
        /// <summary>
        /// The stats this object modifies
        /// </summary>
        private int sm_Stats = -1;
        /// <summary>
        /// The duration in milliseconds this statModifier lasts for
        /// </summary>
        public int sm_Duration = 0;
        /// <summary>
        /// The time between instances where the modification is applied (a tick)
        /// </summary>
        public int sm_TickDuration = 0;
        /// <summary>
        /// The damage applied at the time of the tick
        /// </summary>
        public int sm_Damage = 0;
        /// <summary>
        /// The amount applied to health at the time of the tick
        /// </summary>
        public int sm_Health = 0;

        private Stats stats;

        #region Properties

        public Stats @Stats {
            get {
                if (stats == null && sm_Stats != -1) {
                    stats = Stats.Read(sm_Stats);
                }
                return stats;
            }
        }

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static StatModifier Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<StatModifier> Read() {
            var lst = new List<StatModifier>();
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

        public new static StatModifier ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new StatModifier();
                    obj.sm_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    //obj.sm_Constitution = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Constitution");
                    //obj.sm_Strength = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Strength");
                    //obj.sm_Hardness = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Hardness");
                    //obj.sm_Creativity = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Creativity");
                    //obj.sm_Agility = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Agility");
                    //obj.sm_Wisdom = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Wisdom");
                    //obj.sm_Intellect = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Intellect");
                    //obj.sm_Clarity = GeneralOps.ConvertDatabaseField<int>(dic, "sm_Clarity");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("StatModifier", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static StatModifier Save(StatModifier obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.sm_ID == -1) obj.sm_ID = db.GetNextId(TABLE);
                dic["id"] = obj.sm_ID;
                //dic["sm_Constitution"] = obj.sm_Constitution;
                //dic["sm_Strength"] = obj.sm_Strength;
                //dic["sm_Hardness"] = obj.sm_Hardness;
                //dic["sm_Creativity"] = obj.sm_Creativity;
                //dic["sm_Agility"] = obj.sm_Agility;
                //dic["sm_Wisdom"] = obj.sm_Wisdom;
                //dic["sm_Intellect"] = obj.sm_Intellect;
                //dic["sm_Clarity"] = obj.sm_Clarity;
                return ReadFromDictionary(db.Save(TABLE, obj.sm_ID, dic));
            }
        }

        #endregion
    }
}
