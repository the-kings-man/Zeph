using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zeph.Unity {
    public class GameController : MonoBehaviour {
        public GameObject damageIndicatorTemplate;
        public GameObject projectileTemplate;
        public GameObject indicatorStatBarTemplate;

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
            var obj = Instantiate(damageIndicatorTemplate, position, Quaternion.identity).GetComponent<DamageIndicator>();
            obj.SetText(text);
        }

        public void CreateProjectile(Vector3 position, Character sourceCharater, Character targetCharacter, Zeph.Core.Classes.Attack attack) {
            var obj = Instantiate(projectileTemplate, position, Quaternion.identity).GetComponent<Projectile>();
            obj.sourceCharacter = sourceCharater;
            obj.targetCharacter = targetCharacter;
            obj.attack = attack;
        }

        public IndicatorStatBar CreateIndicatorStatBar(Vector3 position) {
            var obj = Instantiate(indicatorStatBarTemplate, position, Quaternion.identity).GetComponent<IndicatorStatBar>();

            return obj;
        }

    }
}
