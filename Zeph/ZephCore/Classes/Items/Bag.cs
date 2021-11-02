using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Classes.Items {
    public class Bag : Item {
        public int Slots {
            get {
                return (int)MetaData["slots"];
            }
        }
    }
}
