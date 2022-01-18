using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDPlayerNameController : MonoBehaviour {
        private TMPro.TextMeshProUGUI textMesh;

        void Awake() {
            textMesh = GetComponent<TMPro.TextMeshProUGUI>();
        }

        public void HandleRefresh(string name) {
            textMesh.text = name;
        }
    }
}