using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Classes {
    public class PlayerBag : Zeph.Core.Classes.ClassBase<PlayerBag> {
        const string TABLE = "playerBag";

        public int pb_ID = -1;
        public int pb_Player = -1;
        /// <summary>
        /// An item of <see cref="Enums.ItemType.Consumable"/> and <see cref="Enums.ItemSubType.Bag"/>
        /// </summary>
        public int pb_Item = -1;
        /// <summary>
        /// This is initially gathered from the Items metadata. But having this field here will simplify this into the future.
        /// </summary>
        public int pb_Slots = 0;

        private List<PlayerBagSlot> playerBagSlots = null;

        private PlayerBag() {
        }

        public PlayerBag(int playerId, int itemId, int slots) {
            pb_Player = playerId;
            pb_Item = itemId;
            pb_Slots = slots;
        }

        #region Properties

        public List<PlayerBagSlot> PlayerBagSlots {
            get {
                if (playerBagSlots == null) {
                    playerBagSlots = new List<PlayerBagSlot>();
                    var lstPlayerBagSlots = PlayerBagSlot.Read();
                    foreach (var pbs in lstPlayerBagSlots) {
                        if (pbs.pbs_PlayerBag == pb_ID) {
                            playerBagSlots.Add(pbs);
                        }
                    }
                }
                return playerBagSlots;
            }
        }

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static PlayerBag Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<PlayerBag> Read() {
            var lst = new List<PlayerBag>();
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

        public new static PlayerBag ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new PlayerBag();
                    obj.pb_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.pb_Player = GeneralOps.ConvertDatabaseField<int>(dic, "pb_Player");
                    obj.pb_Item = GeneralOps.ConvertDatabaseField<int>(dic, "pb_Item");
                    obj.pb_Slots = GeneralOps.ConvertDatabaseField<int>(dic, "pb_Slots");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("PlayerBag", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public static PlayerBag Save(PlayerBag obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["id"] = obj.pb_ID;
                dic["pb_Player"] = obj.pb_Player;
                dic["pb_Item"] = obj.pb_Item;
                dic["pb_Slots"] = obj.pb_Slots;

                var _obj = ReadFromDictionary(db.Save(TABLE, obj.pb_ID, dic));

                if (obj.pb_ID == -1) obj.pb_ID = _obj.pb_ID;

                if (saveChildren) {
                    if (obj.playerBagSlots != null) {
                        foreach (var pbs in obj.playerBagSlots) {
                            pbs.pbs_PlayerBag = _obj.pb_ID;
                            PlayerBagSlot.Save(pbs);
                        }
                    }
                }

                return _obj;
            }
        }

        #endregion
    }
}
