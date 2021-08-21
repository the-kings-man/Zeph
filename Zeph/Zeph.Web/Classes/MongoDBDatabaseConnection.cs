using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeph.Web.Classes {
    public class MongoDBDatabaseConnection : Zeph.Core.IDatabaseConnection {
        public static string username = "";
        public static string password = "";
        public static string clusterAddress = "";
        public static string databaseName = "";

        private string connectionString {
            get {
                return "mongodb+srv://" + username + ":" + password + "@" + clusterAddress + "/test?w=majority";
            }
        }

        public bool Delete(string tableName, Guid guid) {
            //var client = new MongoClient(connectionString);
            //var database = client.GetDatabase("test");

            //var table = database.GetCollection<>(tableName);
            //table.DeleteOne(new FilterDefinition<>() {
            //    "guid" = guid
            //});

            throw new NotImplementedException();
        }

        public List<Dictionary<string, object>> Read(string tableName) {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Read(string tableName, Guid guid) {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Save(string tableName, Guid guid, Dictionary<string, object> dic) {
            throw new NotImplementedException();
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
        // ~MongoDBDatabaseConnection() {
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

    }
}
