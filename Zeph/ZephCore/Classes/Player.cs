using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public class Player : Zeph.Core.Classes.ClassBase<Player> {
        const string TABLE = "player";

        /// <summary>
        /// GUID of the player
        /// </summary>
        public Guid p_GUID = Guid.Empty;
        /// <summary>
        /// Name of the player
        /// </summary>
        public string p_Name = "";

        #region File Access

        public new static bool Delete(Guid guid) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, guid);
        }

        public new static Player Read(Guid guid) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, guid);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Player> Read() {
            var lst = new List<Player>();
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

        public new static Player ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var p = new Player();
                p.p_GUID = (Guid)dic["p_GUID"];
                p.p_Name = (string)dic["p_Name"];
                return p;
            } else {
                return null;
            }
        }

        public new static Player Save(Player p) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["p_GUID"] = p.p_GUID;
                dic["p_Name"] = p.p_Name;
                return ReadFromDictionary(db.Save("player", p.p_GUID, dic));
            }
        }

        #endregion
    }
}
