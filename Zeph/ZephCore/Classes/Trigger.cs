using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    [Serializable]
    public class Trigger : Zeph.Core.Classes.ClassBase<Trigger> {
        const string TABLE = "trigger";

        /// <summary>
        /// The ID of the Trigger
        /// </summary>
        public int t_ID = -1;
        /// <summary>
        /// Helpful description for what this trigger is for... Mainly for us
        /// </summary>
        public string t_Description = "";
        /// <summary>
        /// The <see cref="NPC.npc_ID"/> of the <see cref="NPC"/> this Trigger is triggered by talking to this NPC
        /// </summary>
        public int t_NPC = -1;
        /// <summary>
        /// The <see cref="Area.a_ID"/> of the <see cref="Area"/> this Trigger is triggered by encountering this Area
        /// </summary>
        public int t_Area = -1;

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Trigger Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Trigger> Read() {
            var lst = new List<Trigger>();
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

        public new static Trigger ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Trigger();
                    obj.t_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.t_Description = GeneralOps.ConvertDatabaseField<string>(dic, "t_Description");
                    obj.t_NPC = GeneralOps.ConvertDatabaseField<int>(dic, "t_NPC");
                    obj.t_Area = GeneralOps.ConvertDatabaseField<int>(dic, "t_Area");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Trigger", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Trigger Save(Trigger obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.t_ID == -1) obj.t_ID = db.GetNextId(TABLE);
                dic["id"] = obj.t_ID;
                dic["t_Description"] = obj.t_Description;
                dic["t_NPC"] = obj.t_NPC;
                dic["t_Area"] = obj.t_Area;
                return ReadFromDictionary(db.Save(TABLE, obj.t_ID, dic));
            }
        }

        #endregion
    }
}
