using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zeph.Core {
    public class JSONDatabaseConnection : IDatabaseConnection {

        private string filePath = "";

        public JSONDatabaseConnection(string filePath) {
            this.filePath = filePath;
        }

        private JObject open() {
            return JObject.Parse(File.ReadAllText(filePath));
        }

        private void save(JObject jsonToFile) {
            File.WriteAllText(filePath, jsonToFile.ToString());
        }

        #region IDatabaseConnection

        public bool Delete(string tableName, int id) {
            var jsonFromFile = open();
            if (jsonFromFile.ContainsKey(tableName)) {
                var table = (JObject)jsonFromFile[tableName];
                if (table.ContainsKey(id.ToString())) {
                    table.Remove(id.ToString());
                    save(jsonFromFile);
                }
            }
            return true;
        }

        public List<Dictionary<string, object>> Read(string tableName) {
            var jsonFromFile = open();
            var lst = new List<Dictionary<string, object>>();
            if (jsonFromFile.ContainsKey(tableName)) {
                var table = (JObject)jsonFromFile[tableName];
                foreach (var pair in table.Properties()) {
                    var dic = new Dictionary<string, object>();
                    foreach (var _pair in ((JObject)pair.Value).Properties()) {
                        dic[_pair.Name] = _pair.Value;
                    }
                    lst.Add(dic);
                }
            }
            return lst;
        }

        public Dictionary<string, object> Read(string tableName, int id) {
            var jsonFromFile = open();
            if (jsonFromFile.ContainsKey(tableName)) {
                var table = (JObject)jsonFromFile[tableName];
                if (table.ContainsKey(id.ToString())) {
                    var jObject = (JObject)table[id.ToString()];
                    var dic = new Dictionary<string, object>();
                    foreach (var pair in jObject) {
                        dic[pair.Key] = pair.Value;
                    }
                    return dic;
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }

        public Dictionary<string, object> Save(string tableName, int id, Dictionary<string, object> dic) {
            var jsonFromFile = open();
            if (!jsonFromFile.ContainsKey(tableName)) {
                jsonFromFile[tableName] = new JObject();
            }

            var table = (JObject)jsonFromFile[tableName];
            var jObject = JObject.FromObject(dic);
            //foreach (var pair in dic) {
            //    jObject[pair.Key] = pair.Value;
            //}
            table[id.ToString()] = jObject;
            save(jsonFromFile);

            return dic;
        }

        public int GetNextId(string tableName) {
            var jsonFromFile = open();
            var n = 0;
            if (jsonFromFile.ContainsKey(tableName)) {
                var table = (JObject)jsonFromFile[tableName];
                while (table.ContainsKey((++n).ToString())) { };
            }
            return n;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~JSONDatabaseConnection() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }
}
