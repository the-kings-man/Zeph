using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Zones {
    public class ZoneSystem {

        #region Event Handling

        public delegate void ZoneEnteredEventHandler(object sender, ZoneEnteredEventArgs e);
        public static event ZoneEnteredEventHandler OnZoneEntered;

        public delegate void ZoneLeftEventHandler(object sender, ZoneLeftEventArgs e);
        public static event ZoneLeftEventHandler ZoneLeft;

        #endregion

        public static void EnterZone(Classes.Player player, Classes.Zone zone) {
            //get all triggers associated with this zone
            var lstTriggers = Zeph.Core.Classes.Trigger.Read();
            Classes.Trigger trigger = null;
            foreach (var t in lstTriggers) {
                if (t.t_Zone == zone.z_ID) {
                    trigger = t;
                    break;
                }
            }

            OnZoneEntered?.Invoke(null, new ZoneEnteredEventArgs() {
                Player = player,
                Area = zone,
                Trigger = trigger
            });
        }

        public static void LeaveZone(Classes.Player player, Classes.Zone zone) {
            var lstTriggers = Zeph.Core.Classes.Trigger.Read();
            Classes.Trigger trigger = null;
            foreach (var t in lstTriggers) {
                if (t.t_Zone == zone.z_ID) {
                    trigger = t;
                    break;
                }
            }

            ZoneLeft?.Invoke(null, new ZoneLeftEventArgs() {
                Player = player,
                Area = zone
            });
        }
    }

    public class ZoneEnteredEventArgs : EventArgs {
        public Classes.Player Player { get; set; }
        public Classes.Zone Area { get; set; }
        public Classes.Trigger Trigger { get; set; }
    }

    public class ZoneLeftEventArgs : EventArgs {
        public Classes.Player Player { get; set; }
        public Classes.Zone Area { get; set; }
        public Classes.Trigger Trigger { get; set; }
    }
}
