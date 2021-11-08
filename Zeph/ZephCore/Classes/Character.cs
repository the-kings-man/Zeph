using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public class Character : Zeph.Core.Classes.ClassBase<Character> {
        const string TABLE = "character";

        /// <summary>
        /// ID of the character
        /// </summary>
        public int c_ID = -1;
        /// <summary>
        /// Name of the character
        /// </summary>
        public string c_Name = "";
        private int c_Stats = -1;
        private int c_Race = -1;

        private Stats stats = null;
        private Race race = null;

        private List<CharacterFaction> characterFactions = null;

        #region Properties

        public Stats @Stats {
            get {
                if (stats == null && c_Stats != -1) {
                    stats = Stats.Read(c_Stats);
                }
                return stats;
            }
        }

        public Race @Race {
            get {
                if (race == null && c_Race != -1) {
                    race = Race.Read(c_Race);
                }
                return race;
            }
        }

        public List<CharacterFaction> CharacterFactions {
            get {
                if (characterFactions == null) {
                    characterFactions = new List<CharacterFaction>();
                    var lstCharacterFactions = CharacterFaction.Read();
                    foreach (var cf in lstCharacterFactions) {
                        if (cf.cf_Character == c_ID) {
                            characterFactions.Add(cf);
                        }
                    }
                }
                return characterFactions;
            }
        }

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Character Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Character> Read() {
            var lst = new List<Character>();
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

        public new static Character ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                var p = new Character();
                p.c_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                p.c_Name = GeneralOps.ConvertDatabaseField<string>(dic, "c_Name");
                p.c_Stats = GeneralOps.ConvertDatabaseField<int>(dic, "c_Stats");
                p.c_Race = GeneralOps.ConvertDatabaseField<int>(dic, "c_Race");
                return p;
            } else {
                return null;
            }
        }

        public static Character Save(Character obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                dic["id"] = obj.c_ID;
                dic["c_Name"] = obj.c_Name;
                dic["c_Stats"] = obj.c_Stats;
                dic["c_Race"] = obj.c_Race;

                var _obj = ReadFromDictionary(db.Save(TABLE, obj.c_ID, dic));

                if (obj.c_ID == -1) obj.c_ID = _obj.c_ID;

                if (saveChildren) {
                    if (obj.c_Stats != -1 && obj.stats != null) Stats.Save(obj.stats);

                    if (obj.characterFactions != null) {
                        foreach (var cf in obj.characterFactions) {
                            cf.cf_Character = _obj.c_ID;
                            CharacterFaction.Save(cf);
                        }
                    }
                }

                return _obj;
            }
        }

        #endregion
    }
}
