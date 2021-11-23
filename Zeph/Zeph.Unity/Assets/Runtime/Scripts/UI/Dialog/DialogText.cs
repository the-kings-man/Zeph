using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZephGame {
    public class DialogText : MonoBehaviour {

        protected TMPro.TextMeshProUGUI textGameObject;

        void Awake() {
        }

        public void Initialise(string text) {
            if (textGameObject == null) textGameObject = this.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();

            textGameObject.text = text;
        }
    }
}
