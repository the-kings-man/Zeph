using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zeph.Unity {
    public class GameController : MonoBehaviour {
        public GameObject damageIndicator;

        private static GameController _instance;
        public static GameController Instance {
            get {
                if (_instance == null) {
                    var obj = FindObjectOfType<GameController>();
                    if (obj) {
                        _instance = obj;
                    } else {
                        throw new Zeph.Core.Classes.ExceptionHandling.GeneralException("Zeph.Unity.GameController", 1, "No game controller found.");
                    }
                }

                return _instance;
            }
        }

        void Awake() {
            Zeph.Core.SystemLocator.CombatSystem = new Zeph.Core.Combat.CombatSystem();
            Zeph.Core.SystemLocator.InventorySystem = new Zeph.Core.Inventory.InventorySystem();
        }

        public void CreateDamageIndicator(string text, Vector3 position) {
            var obj = Instantiate(damageIndicator, position, Quaternion.identity).GetComponent<DamageIndicator>();
            obj.SetText(text);
        }
    }
}
