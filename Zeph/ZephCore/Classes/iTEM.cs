using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Classes {
    public class Item : Zeph.Core.Classes.ClassBase<Item> {
        const string TABLE = "item";

        public int i_ID = -1;
        public string i_Name = "";
        public string i_Description = "";
        public string i_MetaData = "";
        public Enums.ItemType i_Type = Enums.ItemType.None;
        public Enums.ItemSubType i_SubType = Enums.ItemSubType.None;
        public int i_MaxQuantity = 0;

        public Item() {
        }

        #region Properties
        
        public JObject MetaData {
            get {
                return JObject.Parse(i_MetaData);
            }
        }

        #endregion

        #region File Access

        public new static bool Delete(int id) {
            var db = GeneralOps.GetDatabaseConnection();
            return db.Delete(TABLE, id);
        }

        public new static Item Read(int id) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = db.Read(TABLE, id);
                return ReadFromDictionary(dic);
            }
        }

        public new static List<Item> Read() {
            var lst = new List<Item>();
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

        public new static Item ReadFromDictionary(Dictionary<string, object> dic) {
            if (dic != null) {
                try {
                    var obj = new Item();
                    obj.i_ID = GeneralOps.ConvertDatabaseField<int>(dic, "id");
                    obj.i_Name = GeneralOps.ConvertDatabaseField<string>(dic, "i_Name");
                    obj.i_Description = GeneralOps.ConvertDatabaseField<string>(dic, "i_Description");
                    obj.i_MetaData = GeneralOps.ConvertDatabaseField<string>(dic, "i_MetaData");
                    obj.i_Type = (Enums.ItemType)GeneralOps.ConvertDatabaseField<int>(dic, "i_Type");
                    obj.i_SubType = (Enums.ItemSubType)GeneralOps.ConvertDatabaseField<int>(dic, "i_SubType");
                    obj.i_MaxQuantity = GeneralOps.ConvertDatabaseField<int>(dic, "i_MaxQuantity");
                    return obj;
                } catch (Exception ex) {
                    throw new ExceptionHandling.GeneralException("Item", 1, "An error occurred reading dictionary " + GeneralOps.DictionaryToJson(dic) + ". " + ex.Message, ex);
                }
            } else {
                return null;
            }
        }

        public new static Item Save(Item obj, bool saveChildren = true) {
            using (var db = GeneralOps.GetDatabaseConnection()) {
                var dic = new Dictionary<string, object>();
                if (obj.i_ID == -1) obj.i_ID = db.GetNextId(TABLE);
                dic["id"] = obj.i_ID;
                dic["i_Name"] = obj.i_Name;
                dic["i_Description"] = obj.i_Description;
                dic["i_MetaData"] = obj.i_MetaData;
                dic["i_Type"] = (int)obj.i_Type;
                dic["i_SubType"] = (int)obj.i_SubType;
                dic["i_MaxQuantity"] = obj.i_MaxQuantity;

                return ReadFromDictionary(db.Save(TABLE, obj.i_ID, dic));
            }
        }

        #endregion
    }
}
