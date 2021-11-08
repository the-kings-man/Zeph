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
        private int p_Character = -1;

        private List<PlayerBag> playerBags = null;
        private List<PlayerFaction> playerFactions = null;
        private Character character = null;

        #region Properties

        public List<PlayerBag> PlayerBags {
            get {
                if (playerBags == null) {
                    playerBags = new List<PlayerBag>();
                    var lstPlayerBags = PlayerBag.Read();
                    foreach (var pbs in lstPlayerBags) {
                        if (pbs.pb_Player == p_ID) {
                            playerBags.Add(pbs);
                        }
                    }
                }
                return playerBags;
            }
        }

        public List<PlayerFaction> PlayerFactions {
            get {
                if (playerFactions == null) {
                    playerFactions = new List<PlayerFaction>();
                    var lstPlayerFactions = PlayerFaction.Read();
                    foreach (var pf in lstPlayerFactions) {
                        if (pf.pf_Player == p_ID) {
                            playerFactions.Add(pf);
                        }
                    }
                }
                return playerFactions;
            }
        }

        public Character @Character {
            get {
                if (character == null && p_Character != -1) {
                    character = Character.Read(p_Character);
                }
                return character;
            }
        }

        #endregion

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
                p.p_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                p.p_Name = GeneralOps.ConvertDatabaseField<string>(dic, "p_Name");
                p.p_Character = GeneralOps.ConvertDatabaseField<int>(dic, "p_Character");
                return p;
            } else {
                return null;
            }
        }

        public static Player Save(Player obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["id"] = obj.p_ID;
                dic["p_Name"] = obj.p_Name;
                dic["p_Character"] = obj.p_Character;

                var _obj = ReadFromDictionary(db.Save(TABLE, obj.p_ID, dic));

                if (obj.p_ID == -1) obj.p_ID = _obj.p_ID;

                if (saveChildren) {
                    if (obj.p_Character != -1 && obj.character != null) Character.Save(obj.character);

                    if (obj.playerBags != null) {
                        foreach (var pb in obj.playerBags) {
                            pb.pb_Player = _obj.p_ID;
                            PlayerBag.Save(pb);
                        }
                    }

                    if (obj.playerFactions != null) {
                        foreach (var pf in obj.playerFactions) {
                            pf.pf_Player = _obj.p_ID;
                            PlayerFaction.Save(pf);
                        }
                    }
                }

                return _obj;
            }
        }

        #endregion
    }
}
