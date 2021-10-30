using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    [Serializable]
    public class Area : Zeph.Core.Classes.ClassBase<Area> {
        const string TABLE = "area";

        /// <summary>
        /// The ID of the area
        /// </summary>
        public int a_ID = -1;
        /// <summary>
        /// Description of the area
        /// </summary>
        public string a_Description = "";

        #region Properties


        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Area Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Area> Read() {
            var lst = new List<Area>();
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

        public new static Area ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Area();
                    obj.a_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.a_Description = GeneralOps.ConvertDatabaseField<string>(dic, "a_Description");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Area", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Area Save(Area obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.a_ID == -1) obj.a_ID = db.GetNextId(TABLE);
                dic["id"] = obj.a_ID;
                dic["a_Description"] = obj.a_Description;
                return ReadFromDictionary(db.Save(TABLE, obj.a_ID, dic));
            }
        }

        #endregion
    }
}
