using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public interface IClass<T> {
        T Read(Guid guid);
        T ReadFromDictionary(Dictionary<string, object> dic);
        List<T> Read();
        T Save(T obj);
        bool Delete(Guid guid);
    }
}
