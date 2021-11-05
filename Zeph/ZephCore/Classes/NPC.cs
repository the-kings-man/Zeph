using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public class NPC : Zeph.Core.Classes.ClassBase<NPC> {
        const string TABLE = "npc";

        /// <summary>
        /// ID of the npc
        /// </summary>
        public int npc_ID = -1;
        /// <summary>
        /// The name of this npc
        /// </summary>
        public string npc_Name = null;
        private int npc_Character = -1;

        private Character character = null;

        #region "Properties"

        public Character @Character {
            get {
                if (character == null && npc_Character != -1) {
                    character = Character.Read(npc_Character);
                }
                return character;
            }
        }

        #endregion

        #region File Access

        public static new bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public static new NPC Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public static new List<NPC> Read() {
            var lst = new List<NPC>();
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

        public static new NPC ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var npc = new NPC();
                npc.npc_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                npc.npc_Name = GeneralOps.ConvertDatabaseField<string>(dic, "npc_Name");
                npc.npc_Character = GeneralOps.ConvertDatabaseField<int>(dic, "npc_Character");
                return npc;
            } else {
                return null;
            }
        }

        public static NPC Save(NPC obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["id"] = obj.npc_ID;
                dic["npc_Name"] = obj.npc_Name;
                dic["npc_Character"] = obj.npc_Character;

                var _obj = ReadFromDictionary(db.Save(TABLE, obj.npc_ID, dic));

                if (saveChildren) {
                    if (obj.npc_Character != -1 && obj.character != null) Character.Save(obj.character);
                }

                return _obj;
            }
        }

        #endregion
    }
}
