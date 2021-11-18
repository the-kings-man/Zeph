using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zeph.Unity {
    public class DialogResponseText : MonoBehaviour {

        protected TMPro.TextMeshProUGUI textGameObject;
        private Zeph.Core.Classes.DialogResponse dialogResponse;
        private DialogCanvas dialogCanvas;

        void Awake() {
        }

        public void Initialise(Zeph.Core.Classes.DialogResponse dr, DialogCanvas dc) {
            if (textGameObject == null) textGameObject = this.gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();

            dialogResponse = dr;
            dialogCanvas = dc;

            textGameObject.text = dr.dr_Response;
        }

        public void OnClick() {
            dialogCanvas.ResponseReceived(dialogResponse);
        }
    }
}
