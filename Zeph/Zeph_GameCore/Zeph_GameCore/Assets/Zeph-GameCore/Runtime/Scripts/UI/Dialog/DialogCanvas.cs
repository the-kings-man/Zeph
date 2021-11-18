using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZephGame {
    public class DialogCanvas : MonoBehaviour {

        public static DialogCanvas Instance {
            get { return s_Instance; }
        }

        protected static DialogCanvas s_Instance;

        public GameObject dialogTextPrefab;
        public GameObject dialogResponsePrefab;
        public GameObject dialogClose;
        public GameObject background;

        protected GameObject m_DialogText;
        protected List<GameObject> m_DialogResponseTexts = new List<GameObject>();
        protected Canvas m_Canvas;

        private Zeph.Core.Classes.Dialog currentDialog = null;

        void Awake() {
            if (s_Instance == null)
                s_Instance = this;
            else if (s_Instance != this)
                throw new UnityException("There cannot be more than one DialogCanvas script.  The instances are " + s_Instance.name + " and " + name + ".");

            m_Canvas = this.gameObject.GetComponent<Canvas>();
            m_Canvas.gameObject.SetActive(false);
        }

        void Start() {

        }

        public void StartDialog(Zeph.Core.Classes.Dialog dialog) {
            StopDialog(); //make sure dialog is stopped

            currentDialog = dialog;

            m_DialogText = Instantiate(dialogTextPrefab);
            var dialogText = m_DialogText.GetComponent<DialogText>();
            dialogText.Initialise(dialog.d_Dialog);

            var idx = 0;
            foreach (var dr in dialog.dialogResponses) {
                var _dr = Instantiate(dialogResponsePrefab);
                m_DialogResponseTexts.Add(_dr);
                var dialogResponseText = _dr.GetComponent<DialogResponseText>();
                dialogResponseText.Initialise(dr, this);

                var tmp = _dr.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                tmp.rectTransform.localPosition = new Vector3(0, idx++ * 50, 0);
            }

            m_Canvas.gameObject.SetActive(true);
        }

        public void ResponseReceived(Zeph.Core.Classes.DialogResponse dr) {
            var dsr = Zeph.Core.Dialog.DialogSystem.Respond(dr);
            switch (dsr.type) {
                case Zeph.Core.Enums.DialogResponseType.NextDialog:
                    StartDialog(dsr.nextDialog);
                    break;
                case Zeph.Core.Enums.DialogResponseType.ReceiveQuest:
                    GeneralOps.StartQuest(dsr.quest);
                    StopDialog();
                    break;
                case Zeph.Core.Enums.DialogResponseType.HandInQuest:
                    GeneralOps.HandInQuest(dsr.quest);
                    StopDialog();
                    break;
                case Zeph.Core.Enums.DialogResponseType.Close:
                    StopDialog();
                    break;
            }
        }

        public void StopDialog() {
            if (m_DialogText != null) {
                Destroy(m_DialogText);
                m_DialogText = null;
            }
            foreach (var dr in m_DialogResponseTexts) {
                Destroy(dr);
            }
            m_DialogResponseTexts = new List<GameObject>();

            m_Canvas.gameObject.SetActive(false);

            currentDialog = null;
        }

    }
}
