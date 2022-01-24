using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDHotbarButton : MonoBehaviour
    {
        public string key;
        public int attackId;

        public Transform foregroundCooldown;

        TMPro.TextMeshProUGUI textMesh;

        private void Awake() {
            textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }

        public void HandleButtonRefresh(string _key, int _attackId) {
            key = _key;
            attackId = _attackId;

            textMesh.text = key;
        }

        public void HandleCooldownRefresh(Zeph.Core.Combat.CombatEntity combatEntity) {
            if (combatEntity.cooldowns.ContainsKey(attackId)) {
                var cooldown = combatEntity.cooldowns[attackId];

                var fraction = cooldown.timeLeft / cooldown.timeStart;

                var scale = foregroundCooldown.localScale;
                scale.x = fraction;
                foregroundCooldown.localScale = scale;
            } else {
                if (combatEntity.inGlobalCooldown) {
                    var fraction = combatEntity.globalCooldownTimeLeft / Zeph.Core.Combat.CombatEntity.GLOBAL_ATTACK_COOLDOWN;

                    var scale = foregroundCooldown.localScale;
                    scale.x = fraction;
                    foregroundCooldown.localScale = scale;
                } else {
                    foregroundCooldown.localScale = new Vector3(0, 1, 0);
                }
            }
        }

        public void Click() {
            HotbarController.Instance.KeyPressed(key);
        }
    }
}