using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    public class CharacterFaction : Zeph.Core.Classes.ClassBase<CharacterFaction> {
        const string TABLE = "characterFaction";

        /// <summary>
        /// The ID of the characterFaction
        /// </summary>
        public int cf_ID = -1;
        public int cf_Character = -1;
        public int cf_Faction = -1;

        #region Properties


        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static CharacterFaction Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<CharacterFaction> Read() {
            var lst = new List<CharacterFaction>();
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

        public new static CharacterFaction ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new CharacterFaction();
                    obj.cf_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.cf_Character = GeneralOps.ConvertDatabaseField<int>(dic, "cf_Character");
                    obj.cf_Faction = GeneralOps.ConvertDatabaseField<int>(dic, "cf_Faction");

                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("CharacterFaction", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static CharacterFaction Save(CharacterFaction obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.cf_ID == -1) obj.cf_ID = db.GetNextId(TABLE);
                dic["id"] = obj.cf_ID;
                dic["cf_Character"] = obj.cf_Character;
                dic["cf_Faction"] = obj.cf_Faction;

                return ReadFromDictionary(db.Save(TABLE, obj.cf_ID, dic));
            }
        }

        #endregion
    }
}
