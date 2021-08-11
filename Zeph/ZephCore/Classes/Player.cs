using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    [Serializable]
    public class Player : Zeph.Core.Classes.IClass<Player> {
        /// <summary>
        /// GUID of the player
        /// </summary>
        public Guid p_GUID = Guid.Empty;
        /// <summary>
        /// Name of the player
        /// </summary>
        public string p_Name = "";

        #region File Access

        public bool Delete(Guid guid) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete("player", guid);
        }

        public Player Read(Guid guid) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read("player", guid);
                return ReadFromDictionary(dic);
            }
        }

        public List<Player> Read() {
            var lst = new List<Player>();
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var _lst = db.Read("player");
                foreach (var dic in _lst) {
                    var _dic = ReadFromDictionary(dic);
                    if (_dic != null) {
                        lst.Add(_dic);
                    }
                }
            }
            return lst;
        }

        public Player ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var p = new Player();
                p.p_GUID = (Guid)dic["p_GUID"];
                p.p_Name = (string)dic["p_Name"];
                return p;
            } else {
                return null;
            }
        }

        public Player Save(Player p) {
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
