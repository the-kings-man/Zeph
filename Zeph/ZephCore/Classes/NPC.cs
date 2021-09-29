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
        /// The NPC this object is linked to
        /// </summary>
        public NPC npc_NPC = null;
        /// <summary>
        /// The Dialog this object is linked to
        /// </summary>
        public Dialog npc_Dialog = null;

        #region File Access

        public static new bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete("npc", id);
        }

        public static new NPC Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read("npc", id);
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
                npc.npc_ID = (int)dic["npc_ID"];
                npc.npc_NPC = new NPC() { npc_ID = (int)dic["npc_NPC"] };
                npc.npc_Dialog = new Dialog() { d_ID = (int)dic["npc_Dialog"] };
                return npc;
            } else {
                return null;
            }
        }

        public static new NPC Save(NPC npc) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["npc_ID"] = npc.npc_ID;
                dic["npc_NPC"] = npc.npc_NPC;
                dic["npc_Dialog"] = npc.npc_Dialog;
                return ReadFromDictionary(db.Save("npc", npc.npc_ID, dic));
            }
        }

        #endregion
    }
}
