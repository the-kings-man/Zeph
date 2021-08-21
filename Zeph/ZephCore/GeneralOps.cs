using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public static class GeneralOps {
        public static string JSONDatabaseFile = "C:\\zeph.json";
        public static Type CurrentDatabasePlatform = typeof(JSONDatabaseConnection);

        public static IDatabaseConnection GetDatabaseConnection() {
            if (CurrentDatabasePlatform == typeof(JSONDatabaseConnection)) {
                return new JSONDatabaseConnection(JSONDatabaseFile);
            } else {
                throw new Exception("Invalid database connection type.");
            }
        }
    }
}
