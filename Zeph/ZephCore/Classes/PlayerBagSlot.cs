using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Classes {
    public class PlayerBagSlot : Zeph.Core.Classes.ClassBase<PlayerBagSlot> {
        const string TABLE = "playerBagSlot";

        public int pbs_ID = -1;
        public int pbs_PlayerBag = -1;
        public int pbs_Item = -1;
        /// <summary>
        /// This is initially gathered from the Items metadata. But having this field here will simplify this into the future.
        /// </summary>
        public int pbs_Quantity = 0;

        private PlayerBagSlot() {
        }

        public PlayerBagSlot(int playerBagId) {
            pbs_PlayerBag = playerBagId;
        }

        public PlayerBagSlot(int playerBagId, int itemId, int quantity) {
            pbs_PlayerBag = playerBagId;
            pbs_Item = itemId;
            pbs_Quantity = quantity;
        }

        #region Properties

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static PlayerBagSlot Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<PlayerBagSlot> Read() {
            var lst = new List<PlayerBagSlot>();
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

        public new static PlayerBagSlot ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new PlayerBagSlot();
                    obj.pbs_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.pbs_PlayerBag = GeneralOps.ConvertDatabaseField<int>(dic, "pbs_PlayerBag");
                    obj.pbs_Item = GeneralOps.ConvertDatabaseField<int>(dic, "pbs_Item");
                    obj.pbs_Quantity = GeneralOps.ConvertDatabaseField<int>(dic, "pbs_Quantity");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("PlayerBagSlot", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public static PlayerBagSlot Save(PlayerBagSlot obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.pbs_ID == -1) obj.pbs_ID = db.GetNextId(TABLE);
                dic["id"] = obj.pbs_ID;
                dic["pbs_PlayerBag"] = obj.pbs_PlayerBag;
                dic["pbs_Item"] = obj.pbs_Item;
                dic["pbs_Quantity"] = obj.pbs_Quantity;

                return ReadFromDictionary(db.Save(TABLE, obj.pbs_ID, dic));
            }
        }

        #endregion
    }
}
