using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public class NPC : Zeph.Core.Classes.ClassBase<NPC> {
        /// <summary>
        /// GUID of the npc
        /// </summary>
        public Guid npc_GUID = Guid.Empty;
        /// <summary>
        /// Name of the npc
        /// </summary>
        public string npc_Name = "";

        #region File Access

        public static bool Delete(Guid guid) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete("npc", guid);
        }

        public static NPC Read(Guid guid) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read("npc", guid);
                return ReadFromDictionary(dic);
            }
        }

        public static List<NPC> Read() {
            var lst = new List<NPC>();
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var _lst = db.Read("npc");
                foreach (var dic in _lst) {
                    var _dic = ReadFromDictionary(dic);
                    if (_dic != null) {
                        lst.Add(_dic);
                    }
                }
            }
            return lst;
        }

        public static NPC ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var npc = new NPC();
                npc.npc_GUID = (Guid)dic["npc_GUID"];
                npc.npc_Name = (string)dic["npc_Name"];
                return npc;
            } else {
                return null;
            }
        }

        public static NPC Save(NPC npc) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["npc_GUID"] = npc.npc_GUID;
                dic["npc_Name"] = npc.npc_Name;
                return ReadFromDictionary(db.Save("npc", npc.npc_GUID, dic));
            }
        }

        #endregion
    }
}
