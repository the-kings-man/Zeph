using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    public class Race : Zeph.Core.Classes.ClassBase<Race> {
        const string TABLE = "race";

        /// <summary>
        /// The ID of the race
        /// </summary>
        public int r_ID = -1;
        /// <summary>
        /// Name of the race
        /// </summary>
        public string r_Name = "";
        /// <summary>
        /// Description of the race
        /// </summary>
        public string r_Description = "";

        #region Properties


        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Race Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Race> Read() {
            var lst = new List<Race>();
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

        public new static Race ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Race();
                    obj.r_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.r_Name = GeneralOps.ConvertDatabaseField<string>(dic, "r_Name");
                    obj.r_Description = GeneralOps.ConvertDatabaseField<string>(dic, "r_Description");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Race", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Race Save(Race obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.r_ID == -1) obj.r_ID = db.GetNextId(TABLE);
                dic["id"] = obj.r_ID;
                dic["r_Name"] = obj.r_Name;
                dic["r_Description"] = obj.r_Description;
                return ReadFromDictionary(db.Save(TABLE, obj.r_ID, dic));
            }
        }

        #endregion
    }
}
