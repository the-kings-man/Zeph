using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zeph.Unity {
    public class HotbarController : MonoBehaviour {
        public Player player;

        List<HUDHotbar> lstHotbars;

        private static HotbarController _instance;
        public static HotbarController Instance {
            get {
                if (_instance == null) {
                    var obj = FindObjectOfType<HotbarController>();
                    if (obj) {
                        _instance = obj;
                    } else {
                        throw new Zeph.Core.Classes.ExceptionHandling.GeneralException("Zeph.Unity.HotbarController", 1, "No game controller found.");
                    }
                }

                return _instance;
            }
        }

        private void Awake() {
            lstHotbars = GetComponentsInChildren<HUDHotbar>().ToList<HUDHotbar>();
        }

        public void KeyPressed(string key) {
            var hotbars = GetComponentsInChildren<HUDHotbar>();
            foreach (var hotbar in hotbars) {
                var button = hotbar.GetButtonForKey(key);
                if (button != null) {
                    player.PerformAttack(Zeph.Core.Classes.Attack.Read(button.attackId));
                    return;
                }
            }

            //if (key == 1) {
            //    player.PerformAttack(Zeph.Core.Classes.Attack.Read(1));
            //} else if (key == 2) {
            //    player.PerformAttack(Zeph.Core.Classes.Attack.Read(2));
            //} else if (key == 3) {
            //    player.PerformAttack(Zeph.Core.Classes.Attack.Read(3));
            //} else if (key == 4) {
            //    player.PerformAttack(Zeph.Core.Classes.Attack.Read(4));
            //}
        }

        public void Update() {
            if (player.characterCombat.combatEntity != null) {
                foreach (var hotbar in lstHotbars) hotbar.HandleCooldownRefresh(player.characterCombat.combatEntity);
            }
        }
    }
}
