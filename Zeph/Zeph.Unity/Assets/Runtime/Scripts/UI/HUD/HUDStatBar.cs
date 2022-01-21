using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDStatBar : MonoBehaviour {
        private TMPro.TextMeshProUGUI textMesh;
        public Transform foreground;

        void Awake() {
            textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();

            if (foreground == null) Debug.Log("Must select foreground object on HUDStatBar");
        }

        public void HandleRefresh(bool showText, long currentValue, long maxValue) {
            if (showText) {
                textMesh.text = currentValue + " / " + maxValue;

                if (foreground != null) {
                    var fraction = Mathf.Clamp(currentValue / (float)maxValue, 0f, 1f);

                    var scale = foreground.localScale;
                    scale.x = fraction;
                    foreground.localScale = scale;
                }
            } else {
                textMesh.text = "";
                foreground.localScale = new Vector3(1, 1, 0);
            }
        }
    }
}