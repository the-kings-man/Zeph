using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {

    public class Attack : Zeph.Core.Classes.ClassBase<Attack> {
        const string TABLE = "attack";

        /// <summary>
        /// The ID of the attack
        /// </summary>
        public int a_ID = -1;
        /// <summary>
        /// Name of the attack
        /// </summary>
        public string a_Name = "";
        /// <summary>
        /// Description of the attack
        /// </summary>
        public string a_Description = "";
        /// <summary>
        /// The type of attack
        /// </summary>
        public Enums.AttackType a_AttackType = Enums.AttackType.None;
        /// <summary>
        /// The distance this attack can be used from
        /// </summary>
        public int a_Distance = 0;
        /// <summary>
        /// Base damage of the attack upon impact
        /// </summary>
        public int a_Damage = 0;
        /// <summary>
        /// How long in milliseconds it takes for this attack to be prepared/cast
        /// </summary>
        public int a_PreparationDuration = 0;
        /// <summary>
        /// If this attack results in stat modification or a damage over time, this can be applied here
        /// </summary>
        public int a_StatModifier = 0;
        /// <summary>
        /// The cooldown this attack requires before uses. Set to 0 for no cooldown. Value is in milliseconds.
        /// </summary>
        public int a_Cooldown = 0;

        #region Properties


        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Attack Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Attack> Read() {
            var lst = new List<Attack>();
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

        public new static Attack ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Attack();
                    obj.a_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.a_Name = GeneralOps.ConvertDatabaseField<string>(dic, "a_Name");
                    obj.a_Description = GeneralOps.ConvertDatabaseField<string>(dic, "a_Description");
                    obj.a_AttackType = (Enums.AttackType)GeneralOps.ConvertDatabaseField<int>(dic, "a_AttackType");
                    obj.a_Distance = GeneralOps.ConvertDatabaseField<int>(dic, "a_Distance");
                    obj.a_Damage = GeneralOps.ConvertDatabaseField<int>(dic, "a_Damage");
                    //obj.a_IsDOT = GeneralOps.ConvertDatabaseField<bool>(dic, "a_IsDOT");
                    //obj.a_DOTDamage = GeneralOps.ConvertDatabaseField<int>(dic, "a_DOTDamage");
                    //obj.a_DOTTimeBetweenTicks = GeneralOps.ConvertDatabaseField<int>(dic, "a_DOTTimeBetweenTicks");
                    //obj.a_DOTDuration = GeneralOps.ConvertDatabaseField<int>(dic, "a_DOTDuration");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Attack", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Attack Save(Attack obj) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.a_ID == -1) obj.a_ID = db.GetNextId(TABLE);
                dic["id"] = obj.a_ID;
                dic["a_Name"] = obj.a_Name;
                dic["a_Description"] = obj.a_Description;
                dic["a_AttackType"] = (int)obj.a_AttackType;
                dic["a_Distance"] = obj.a_Distance;
                dic["a_Damage"] = obj.a_Damage;
                //dic["a_IsDOT"] = obj.a_IsDOT;
                //dic["a_DOTDamage"] = obj.a_DOTDamage;
                //dic["a_DOTTimeBetweenTicks"] = obj.a_DOTTimeBetweenTicks;
                //dic["a_DOTDuration"] = obj.a_DOTDuration;
                return ReadFromDictionary(db.Save(TABLE, obj.a_ID, dic));
            }
        }

        #endregion
    }
}
