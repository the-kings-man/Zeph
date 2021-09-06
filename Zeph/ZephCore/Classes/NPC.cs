using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public class NPC : Zeph.Core.Classes.ClassBase<NPC> {
        const string TABLE = "npc";

        /// <summary>
        /// GUID of the npc
        /// </summary>
        public Guid npc_GUID = Guid.Empty;
        /// <summary>
        /// The NPC this object is linked to
        /// </summary>
        public NPC npc_NPC = null;
        /// <summary>
        /// The Dialog this object is linked to
        /// </summary>
        public Dialog npc_Dialog = null;

        #region File Access

        public static new bool Delete(Guid guid) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete("npc", guid);
        }

        public static new NPC Read(Guid guid) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read("npc", guid);
                return ReadFromDictionary(dic);
            }
        }

        public static new List<NPC> Read() {
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

        public static new NPC ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var npc = new NPC();
                npc.npc_GUID = (Guid)dic["npc_GUID"];
                npc.npc_NPC = new NPC() { npc_GUID = (Guid)dic["npc_NPC"] };
                npc.npc_Dialog = new Dialog() { d_GUID = (Guid)dic["npc_Dialog"] };
                return npc;
            } else {
                return null;
            }
        }

        public static new NPC Save(NPC npc) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["npc_GUID"] = npc.npc_GUID;
                dic["npc_NPC"] = npc.npc_NPC;
                dic["npc_Dialog"] = npc.npc_Dialog;
                return ReadFromDictionary(db.Save("npc", npc.npc_GUID, dic));
            }
        }

        #endregion
    }
}
