using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public interface IDatabaseConnection : IDisposable {
        bool Delete(string tableName, int id);
        List<Dictionary<string, object>> Read(string tableName);
        Dictionary<string, object> Read(string tableName, int id);
        Dictionary<string, object> Save(string tableName, int id, Dictionary<string, object> dic);
    }
}
