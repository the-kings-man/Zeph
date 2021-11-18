using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZephGame {
    public class HUDPlayerQuestCanvas : MonoBehaviour {

        public GameObject playerQuestHUDTextPrefab;

        protected Canvas m_Canvas;

        private List<HUDPlayerQuestText> playerQuestTexts = null;

        void Awake() {
            m_Canvas = this.gameObject.GetComponent<Canvas>();

            RefreshQuestList();
        }

        void Start() {
        }

        public void RefreshQuestList() {
            ClearQuestList();

            playerQuestTexts = new List<HUDPlayerQuestText>();

            var lstPlayerQuest = Zeph.Core.Classes.PlayerQuest.Read();
            for (var i = 0; i < lstPlayerQuest.Count; i++) {
                var pq = lstPlayerQuest[i];

                if (!pq.pq_HandedIn) { //only show non-handed in quests
                    var _pq = Instantiate(playerQuestHUDTextPrefab, m_Canvas.transform);
                    var playerQuestHUDText = _pq.GetComponent<HUDPlayerQuestText>();
                    playerQuestHUDText.Initialise(pq);

                    var rectTransform = playerQuestHUDText.GetComponent<RectTransform>();
                    rectTransform.Translate(0, -60 * i, 0);
                }
            }
        }

        public void ClearQuestList() {
            if (playerQuestTexts != null) {
                foreach (var _pq in playerQuestTexts) {
                    Destroy(_pq);
                }
            }
        }
    }
}
