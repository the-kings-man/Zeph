using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zeph.Unity {
    public class HotbarController : MonoBehaviour {
        public Player player;

        public void ButtonPressed(int button) {
            if (button == 1) {
                player.PerformAttack(Zeph.Core.Classes.Attack.Read(1));
            } else if (button == 2) {
                player.PerformAttack(Zeph.Core.Classes.Attack.Read(2));
            } else if (button == 3) {
                player.PerformAttack(Zeph.Core.Classes.Attack.Read(3));
            }
        }
    }
}
