using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Classes {
    public class PlayerCurrency : Zeph.Core.Classes.ClassBase<PlayerCurrency> {
        const string TABLE = "playerCurrency";

        public int pc_ID = -1;
        public int pc_Player = -1;
        public int pc_Currency = -1;
        public long pc_Amount = 0L;

        private PlayerCurrency() {
        }

        public PlayerCurrency(int playerId, int currencyId) {
            pc_Player = playerId;
            pc_Currency = currencyId;
        }

        #region Properties

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static PlayerCurrency Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<PlayerCurrency> Read() {
            var lst = new List<PlayerCurrency>();
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

        public new static PlayerCurrency ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new PlayerCurrency();
                    obj.pc_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.pc_Player = GeneralOps.ConvertDatabaseField<int>(dic, "pc_Player");
                    obj.pc_Currency = GeneralOps.ConvertDatabaseField<int>(dic, "pc_Currency");
                    obj.pc_Amount = GeneralOps.ConvertDatabaseField<long>(dic, "pc_Amount");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("PlayerCurrency", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public static PlayerCurrency Save(PlayerCurrency obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.pc_ID == -1) obj.pc_ID = db.GetNextId(TABLE);
                dic["id"] = obj.pc_ID;
                dic["pc_Player"] = obj.pc_Player;
                dic["pc_Currency"] = obj.pc_Currency;
                dic["pc_Amount"] = obj.pc_Amount;

                return ReadFromDictionary(db.Save(TABLE, obj.pc_ID, dic));
            }
        }

        #endregion
    }
}
