using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public class Player : Zeph.Core.Classes.ClassBase<Player> {
        const string TABLE = "player";

        /// <summary>
        /// ID of the player
        /// </summary>
        public int p_ID = -1;
        /// <summary>
        /// Name of the player
        /// </summary>
        public string p_Name = "";

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Player Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
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
                p.p_ID = Convert.ToInt32(dic["id"]);
                p.p_Name = Convert.ToString(dic["p_Name"]);
                return p;
            } else {
                return null;
            }
        }

        public new static Player Save(Player p) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (p.p_ID == -1) p.p_ID = db.GetNextId(TABLE);
                dic["id"] = p.p_ID;
                dic["p_Name"] = p.p_Name;
                return ReadFromDictionary(db.Save(TABLE, p.p_ID, dic));
            }
        }

        #endregion
    }
}
