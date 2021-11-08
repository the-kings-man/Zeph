using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Classes {
    public class PlayerFaction : Zeph.Core.Classes.ClassBase<PlayerFaction> {
        const string TABLE = "playerFaction";

        public int pf_ID = -1;
        public int pf_Player = -1;
        public int pf_Faction = -1;
        public int pf_Reputation = 0;

        public PlayerFaction() {
        }

        #region Properties

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static PlayerFaction Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<PlayerFaction> Read() {
            var lst = new List<PlayerFaction>();
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

        public new static PlayerFaction ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new PlayerFaction();
                    obj.pf_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.pf_Player = GeneralOps.ConvertDatabaseField<int>(dic, "pf_Player");
                    obj.pf_Faction = GeneralOps.ConvertDatabaseField<int>(dic, "pf_Faction");
                    obj.pf_Reputation = GeneralOps.ConvertDatabaseField<int>(dic, "pf_Reputation");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("PlayerFaction", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public static PlayerFaction Save(PlayerFaction obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["id"] = obj.pf_ID;
                dic["pf_Player"] = obj.pf_Player;
                dic["pf_Faction"] = obj.pf_Faction;
                dic["pf_Reputation"] = obj.pf_Reputation;

                var _obj = ReadFromDictionary(db.Save(TABLE, obj.pf_ID, dic));

                if (obj.pf_ID == -1) obj.pf_ID = _obj.pf_ID;

                if (saveChildren) {

                }

                return _obj;
            }
        }

        #endregion
    }
}
