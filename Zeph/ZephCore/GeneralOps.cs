using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public static class GeneralOps {
        public static string JSONDatabaseFile = "E:\\Source\\Zeph_source\\Zeph\\testdatabase.json";
        public static Type CurrentDatabasePlatform = typeof(JSONDatabaseConnection);

        public static IDatabaseConnection GetDatabaseConnection() {
            if (CurrentDatabasePlatform == typeof(JSONDatabaseConnection)) {
                return new JSONDatabaseConnection(JSONDatabaseFile);
            } else {
                throw new Exception("Invalid database connection type.");
            }
        }

        /// <summary>
        /// This is to handle timezones should the need arise.
        /// </summary>
        public static DateTime Now {
            get {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Converts a value from the database to the desired value. Offers helpful error handling.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T ConvertDatabaseField<T>(Dictionary<string, object> dic, string field) {
            try {
                if (dic.ContainsKey(field)) {
                    if (dic[field] is JValue) {
                        return ((JValue)dic[field]).ToObject<T>();
                    } else {
                        return (T)dic[field];
                    }
                } else {
                    throw new Exception("Field " + field + " was not found in the dictionary.");
                }
            } catch (InvalidCastException ice) {
                if (dic[field] is JValue) {
                    throw new Exception("The field " + field + " was not of the type " + typeof(T).ToString() + " instead it is of the type JValue (" + ((JValue)dic[field]).Type.ToString() + ", " + dic[field].ToString() + "). Cannot convert."); 
                } else {
                    throw new Exception("The field " + field + " was not of the type " + typeof(T).ToString() + " instead it is of the type " + dic[field].GetType().ToString() + " (" + dic[field].ToString() + "). Cannot convert.");
                }
            }
        }

        /// <summary>
        /// Whether or not the passed field is null. If field is missing will return true.
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool IsDatabaseFieldNull(Dictionary<string, object> dic, string field) {
            try {
                if (dic.ContainsKey(field)) {
                    if (dic[field] is JValue) {
                        return ((JValue)dic[field]).Type == JTokenType.Null;
                    } else {
                        return dic[field] == null;
                    }
                } else {
                    return true;
                }
            } catch (Exception ex) {
                throw new Exception("Could not determine if the field " + field + " was null. " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Converts a dictionary to a json string. Makes it nice for error handling.
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string DictionaryToJson(Dictionary<string, object> dic) {
            var jObject = new JObject();
            foreach (var pair in dic) {
                jObject[pair.Key] = (JToken)pair.Value;
            }
            return jObject.ToString();
        }
    }
}
