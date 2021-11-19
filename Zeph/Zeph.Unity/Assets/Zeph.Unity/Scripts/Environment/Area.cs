using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ZephGame {
    public class Area : MonoBehaviour {
        public int id;

        public void Awake() {
            if (id <= 0) {
                throw new Exception("Area must have an id.");
            }
        }

        public void OnEnter() {
            //Check if there is a trigger associated with this area, trigger progress
            var lstTriggers = Zeph.Core.Classes.Trigger.Read();
            foreach (var t in lstTriggers) {
                if (t.t_Zone == id) {
                    //trigger for this area!
                    Zeph.Core.Questing.QuestingSystem.TriggerProgress(GeneralOps.CurrentPlayer, t, 1);
                }
            }
        }
    }
}
