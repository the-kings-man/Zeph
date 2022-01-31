using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Great idea from https://www.youtube.com/watch?v=I2j6mQpCrWE
    /// </remarks></remarsk>
    public class IndicatorStatBar : MonoBehaviour {
        private TMPro.TextMeshProUGUI textMesh;
        public Transform foreground;

        // Start is called before the first frame update
        void Awake() {
            textMesh = GetComponentInChildren<TMPro.TextMeshProUGUI>();

            if (foreground == null) Debug.Log("Must select foreground object on IndicatorStatBar " + this.name);

            transform.LookAt(2 * transform.position - Camera.main.transform.position);
        }

        public void HandleRefresh(bool showText, long currentValue, long maxValue, Vector3 position) {
            transform.LookAt(2 * transform.position - Camera.main.transform.position);

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

            transform.position = position;
        }
    }
}