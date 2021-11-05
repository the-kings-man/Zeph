using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Classes {
    public class Currency : Zeph.Core.Classes.ClassBase<Currency> {
        const string TABLE = "currency";

        public int c_ID = -1;
        public string c_Name = "";
        public string c_Description = "";

        public Currency() {
        }

        #region Properties

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Currency Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Currency> Read() {
            var lst = new List<Currency>();
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

        public new static Currency ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Currency();
                    obj.c_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.c_Name = GeneralOps.ConvertDatabaseField<string>(dic, "c_Name");
                    obj.c_Description = GeneralOps.ConvertDatabaseField<string>(dic, "c_Description");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Currency", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public static Currency Save(Currency obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.c_ID == -1) obj.c_ID = db.GetNextId(TABLE);
                dic["id"] = obj.c_ID;
                dic["c_Name"] = obj.c_Name;
                dic["c_Description"] = obj.c_Description;

                return ReadFromDictionary(db.Save(TABLE, obj.c_ID, dic));
            }
        }

        #endregion
    }
}
