using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    public class Zone : Zeph.Core.Classes.ClassBase<Zone> {
        const string TABLE = "zone";

        /// <summary>
        /// The ID of the zone
        /// </summary>
        public int z_ID = -1;
        /// <summary>
        /// Description of the zone
        /// </summary>
        public string z_Description = "";

        #region Properties


        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Zone Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Zone> Read() {
            var lst = new List<Zone>();
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

        public new static Zone ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Zone();
                    obj.z_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.z_Description = GeneralOps.ConvertDatabaseField<string>(dic, "z_Description");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Zone", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Zone Save(Zone obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.z_ID == -1) obj.z_ID = db.GetNextId(TABLE);
                dic["id"] = obj.z_ID;
                dic["z_Description"] = obj.z_Description;
                return ReadFromDictionary(db.Save(TABLE, obj.z_ID, dic));
            }
        }

        #endregion
    }
}
