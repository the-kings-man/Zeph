using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    /// <summary>
    /// The class which handles the discussion from the enrivonment to the player
    /// </summary>
    public class Dialog : Zeph.Core.Classes.ClassBase<Dialog> {
        const string TABLE = "dialog";

        /// <summary>
        /// GUID of the dialog
        /// </summary>
        public Guid d_GUID = Guid.Empty;
        /// <summary>
        /// Name of the dialog
        /// </summary>
        public string d_Name = "";
        /// <summary>
        /// Text to display to the player
        /// </summary>
        public string d_Dialog = "";
        /// <summary>
        /// The NPC associated with this dialog
        /// </summary>
        public NPC d_NPC = null;

        #region File Access

        public new static bool Delete(Guid guid) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, guid);
        }

        public new static Dialog Read(Guid guid) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, guid);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Dialog> Read() {
            var lst = new List<Dialog>();
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

        public new static Dialog ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var d = new Dialog();
                d.d_GUID = (Guid)dic["d_GUID"];
                d.d_Name = (string)dic["d_Name"];
                d.d_Dialog = (string)dic["d_Dialog"];
                d.d_NPC = dic["d_NPC"] == null ? null : new NPC() { npc_GUID = (Guid)dic["d_NPC"] };
                return d;
            } else {
                return null;
            }
        }

        public new static Dialog Save(Dialog d) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["d_GUID"] = d.d_GUID;
                dic["d_Name"] = d.d_Name;
                dic["d_Dialog"] = d.d_Dialog;
                dic["d_NPC"] = d.d_NPC == null ? null : (object)d.d_NPC.npc_GUID;
                return ReadFromDictionary(db.Save("dialog", d.d_GUID, dic));
            }
        }

        #endregion
    }
}
