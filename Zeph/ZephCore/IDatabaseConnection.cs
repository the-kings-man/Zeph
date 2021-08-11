using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core {
    public interface IDatabaseConnection : IDisposable {
        bool Delete(string tableName, Guid guid);
        List<Dictionary<string, object>> Read(string tableName);
        Dictionary<string, object> Read(string tableName, Guid guid);
        Dictionary<string, object> Save(string tableName, Guid guid, Dictionary<string, object> dic);
    }
}
