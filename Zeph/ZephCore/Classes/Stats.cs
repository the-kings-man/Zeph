using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    public class Stats : Zeph.Core.Classes.ClassBase<Stats> {
        const string TABLE = "stats";

        /// <summary>
        /// The ID of the stats
        /// </summary>
        public int s_ID = -1;
        /// <summary>
        /// Their makeup, physical essence. Relates to health in game and general ability to keep going, and resistance to weariness
        /// </summary>
        public int s_Constitution = 0;
        /// <summary>
        /// How strong they are. Relates to melee strength, and bow strength, ability to lift. Physical strength.
        /// </summary>
        public int s_Strength = 0;
        /// <summary>
        /// How resistant they are to physical attacks. Thick skin, soft heart. Rubs off on those around them and increases their animals resistance too. A hard, bear of a man.
        /// </summary>
        public int s_Hardness = 0;
        /// <summary>
        /// Ability to come up with neat solutions to situational problems. Increased effectiveness with plant attacks, quicker to release from binds.
        /// </summary>
        public int s_Creativity = 0;
        /// <summary>
        /// Skill with thrown weapons, ability to dodge/escape traps and physical attacks. Speed.
        /// </summary>
        public int s_Agility = 0;
        /// <summary>
        /// Ability to think deeply and critically about whatever is at hand, and see the absolute truth in the situation. Increase base magic attack/size, plant attack/size, less chance of missing attacks.
        /// </summary>
        public int s_Wisdom = 0;
        /// <summary>
        /// A vast an pin-pointed knowledge about the world and it's functionality. Increase magical power, increase chance of critical hits, increase plant power.
        /// </summary>
        public int s_Intellect = 0;
        /// <summary>
        /// How resistant they are to magical attacks/effects, resistance to distraction.
        /// </summary>
        public int s_Clarity = 0;

        #region Properties


        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Stats Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Stats> Read() {
            var lst = new List<Stats>();
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

        public new static Stats ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Stats();
                    obj.s_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.s_Constitution = GeneralOps.ConvertDatabaseField<int>(dic, "s_Constitution");
                    obj.s_Strength = GeneralOps.ConvertDatabaseField<int>(dic, "s_Strength");
                    obj.s_Hardness = GeneralOps.ConvertDatabaseField<int>(dic, "s_Hardness");
                    obj.s_Creativity = GeneralOps.ConvertDatabaseField<int>(dic, "s_Creativity");
                    obj.s_Agility = GeneralOps.ConvertDatabaseField<int>(dic, "s_Agility");
                    obj.s_Wisdom = GeneralOps.ConvertDatabaseField<int>(dic, "s_Wisdom");
                    obj.s_Intellect = GeneralOps.ConvertDatabaseField<int>(dic, "s_Intellect");
                    obj.s_Clarity = GeneralOps.ConvertDatabaseField<int>(dic, "s_Clarity");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Stats", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Stats Save(Stats obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.s_ID == -1) obj.s_ID = db.GetNextId(TABLE);
                dic["id"] = obj.s_ID;
                dic["s_Constitution"] = obj.s_Constitution;
                dic["s_Strength"] = obj.s_Strength;
                dic["s_Hardness"] = obj.s_Hardness;
                dic["s_Creativity"] = obj.s_Creativity;
                dic["s_Agility"] = obj.s_Agility;
                dic["s_Wisdom"] = obj.s_Wisdom;
                dic["s_Intellect"] = obj.s_Intellect;
                dic["s_Clarity"] = obj.s_Clarity;
                return ReadFromDictionary(db.Save(TABLE, obj.s_ID, dic));
            }
        }

        #endregion
    }
}
