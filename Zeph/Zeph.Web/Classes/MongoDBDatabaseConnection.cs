using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zeph.Web.Classes {
    public class MongoDBDatabaseConnection : Zeph.Core.IDatabaseConnection {
        //The Secrets class has not been checked into the remote repo as this data probably shouldn't be stored on an open source remote repo. Instead, create and implement a Secrets class with whatever is needed here.
        public static string username = Secrets.mongoDBUsername;
        public static string password = Secrets.mongoDBPassword;
        public static string clusterAddress = Secrets.mongoDBClusterAddress;
        public static string databaseName = Secrets.mongoDBDatabaseName;

        private string connectionString {
            get {
                return "mongodb+srv://" + username + ":" + password + "@" + clusterAddress + "/test?w=majority";
            }
        }

        public bool Delete(string tableName, int id) {
            //var client = new MongoClient(connectionString);
            //var database = client.GetDatabase("test");

            //var table = database.GetCollection<>(tableName);
            //table.DeleteOne(new FilterDefinition<>() {
            //    "int" = int
            //});

            throw new NotImplementedException();
        }

        public List<Dictionary<string, object>> Read(string tableName) {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Read(string tableName, int id) {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> Save(string tableName, int id, Dictionary<string, object> dic) {
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
