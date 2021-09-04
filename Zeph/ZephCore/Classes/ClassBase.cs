using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes {
    public class ClassBase<T> {
        public static T Read(Guid guid) => throw new NotImplementedException();
        public static T ReadFromDictionary(Dictionary<string, object> dic) => throw new NotImplementedException();
        public static List<T> Read() => throw new NotImplementedException();
        public static T Save(T obj) => throw new NotImplementedException();
        public static bool Delete(Guid guid) => throw new NotImplementedException();
    }
}
