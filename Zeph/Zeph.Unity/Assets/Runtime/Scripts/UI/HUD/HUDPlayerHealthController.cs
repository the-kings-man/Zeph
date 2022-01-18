using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDPlayerHealthController : MonoBehaviour {
        private TMPro.TextMeshProUGUI textMesh;
        public Transform foreground;

        void Awake() {
            textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }

        public void HandleRefresh(Zeph.Core.Combat.CombatEntity combatEntity) {
            if (combatEntity != null) {
                textMesh.text = combatEntity.currentHealth + " / " + combatEntity.maxHealth;
            } else {
                textMesh.text = "";
            }

            if (foreground != null && combatEntity != null) {
                var fraction = Mathf.Clamp(combatEntity.currentHealth / (float)combatEntity.maxHealth, 0f, 1f);

                var scale = foreground.localScale;
                scale.x = fraction;
                foreground.localScale = scale;
            } else {
                var scale = foreground.localScale;
                scale.x = 1;
                foreground.localScale = scale;
            }
        }
    }
}