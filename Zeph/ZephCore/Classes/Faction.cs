using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    public class Faction : Zeph.Core.Classes.ClassBase<Faction> {
        const string TABLE = "faction";

        /// <summary>
        /// The ID of the faction
        /// </summary>
        public int f_ID = -1;
        /// <summary>
        /// Name of the faction
        /// </summary>
        public string f_Name = "";
        /// <summary>
        /// Description of the faction
        /// </summary>
        public string f_Description = "";

        #region Properties


        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Faction Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Faction> Read() {
            var lst = new List<Faction>();
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

        public new static Faction ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Faction();
                    obj.f_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.f_Name = GeneralOps.ConvertDatabaseField<string>(dic, "f_Name");
                    obj.f_Description = GeneralOps.ConvertDatabaseField<string>(dic, "f_Description");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Faction", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Faction Save(Faction obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.f_ID == -1) obj.f_ID = db.GetNextId(TABLE);
                dic["id"] = obj.f_ID;
                dic["f_Name"] = obj.f_Name;
                dic["f_Description"] = obj.f_Description;
                return ReadFromDictionary(db.Save(TABLE, obj.f_ID, dic));
            }
        }

        #endregion
    }
}
