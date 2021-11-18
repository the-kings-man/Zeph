using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zeph.Unity {
    public class QuestCanvas : MonoBehaviour {

        public static QuestCanvas Instance {
            get { return s_Instance; }
        }

        protected static QuestCanvas s_Instance;

        public TMPro.TextMeshProUGUI questHeaderText;
        public TMPro.TextMeshProUGUI questBodyText;
        public GameObject questAccept;
        public GameObject questHandIn;
        public GameObject questDecline;
        public GameObject questClose;
        public GameObject background;

        protected Canvas m_Canvas;

        private Zeph.Core.Classes.Quest currentQuest = null;

        void Awake() {
            if (s_Instance == null)
                s_Instance = this;
            else if (s_Instance != this)
                throw new UnityException("There cannot be more than one QuestCanvas script.  The instances are " + s_Instance.name + " and " + name + ".");

            m_Canvas = this.gameObject.GetComponent<Canvas>();
            m_Canvas.gameObject.SetActive(false);
        }

        void Start() {
        }

        public void ShowQuest(Zeph.Core.Classes.Quest quest, bool canAccept) {
            CloseCanvas();

            currentQuest = quest;

            questHeaderText.text = quest.q_Name;
            questBodyText.text = quest.q_Description;

            if (canAccept) {
                questAccept.SetActive(true);
                questDecline.SetActive(true);
            } else {
                questAccept.SetActive(false);
                questDecline.SetActive(false);
            }
            questHandIn.SetActive(false);

            m_Canvas.gameObject.SetActive(true);
        }

        public void ShowHandInQuest(Zeph.Core.Classes.Quest quest) {
            CloseCanvas();

            currentQuest = quest;

            questHeaderText.text = quest.q_Name;
            questBodyText.text = quest.q_Description;

            questAccept.SetActive(false);
            questDecline.SetActive(false);
            questHandIn.SetActive(true);

            m_Canvas.gameObject.SetActive(true);
        }

        public void Accept() {
            Zeph.Core.Questing.QuestingSystem.Start(GeneralOps.CurrentPlayer, currentQuest);

            CloseCanvas();
        }

        public void HandIn() {
             Zeph.Core.Questing.QuestingSystem.HandIn(GeneralOps.CurrentPlayer, currentQuest);

            CloseCanvas();
        }

        public void CloseCanvas() {
            currentQuest = null;

            m_Canvas.gameObject.SetActive(false);
        }

    }
}
