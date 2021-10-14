using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes.ExceptionHandling {
    public class GeneralException : Exception {
        public string Class = "";
        public int Id = 0;

        private GeneralException() {

        }

        public GeneralException(string cls, int id, string message) : base(message) {
            Class = cls;
            Id = id;
        }

        public GeneralException(string cls, int id, string message, Exception innerException) : base(message, innerException) {
            Class = cls;
            Id = id;
        }
    }
}
