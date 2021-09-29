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
        /// ID of the dialog
        /// </summary>
        public int d_ID = -1;
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
        /// <summary>
        /// Whether or not this dialog is the initial dialog in a dialog sequence
        /// </summary>
        public bool d_IsInitial = true;

        public List<DialogResponse> dialogResponses {
            get {
                var lstDR = DialogResponse.Read();
                var _dialogResponses = new List<DialogResponse>();
                foreach (var dr in lstDR) {
                    if (dr.dr_Dialog.d_ID == d_ID) {
                        _dialogResponses.Add(dr);
                    }
                }
                return _dialogResponses;
            }
        }

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Dialog Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
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
                d.d_ID = (int)dic["d_ID"];
                d.d_Name = (string)dic["d_Name"];
                d.d_Dialog = (string)dic["d_Dialog"];
                d.d_NPC = dic["d_NPC"] == null ? null : new NPC() { npc_ID = (int)dic["d_NPC"] };
                d.d_IsInitial = (bool)dic["d_IsInitial"];
                return d;
            } else {
                return null;
            }
        }

        public new static Dialog Save(Dialog d) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["d_ID"] = d.d_ID;
                dic["d_Name"] = d.d_Name;
                dic["d_Dialog"] = d.d_Dialog;
                dic["d_NPC"] = d.d_NPC == null ? null : (object)d.d_NPC.npc_ID;
                dic["d_IsInitial"] = d.d_IsInitial;
                return ReadFromDictionary(db.Save("dialog", d.d_ID, dic));
            }
        }

        #endregion
    }
}
