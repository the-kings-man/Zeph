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
                npc.npc_ID = Convert.ToInt32(dic["id"]);
                npc.npc_Name = Convert.ToString(dic["npc_Name"]);
                return npc;
            } else {
                return null;
            }
        }

        public static new NPC Save(NPC npc) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (npc.npc_ID == -1) npc.npc_ID = db.GetNextId(TABLE);
                dic["id"] = npc.npc_ID;
                dic["npc_Name"] = npc.npc_Name;
                return ReadFromDictionary(db.Save(TABLE, npc.npc_ID, dic));
            }
        }

        #endregion
    }
}
