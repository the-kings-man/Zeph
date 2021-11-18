using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zeph.Unity {
    public class HUDPlayerQuestText : MonoBehaviour {

        public TMPro.TextMeshProUGUI textQuestText;
        public TMPro.TextMeshProUGUI textObjective;

        private Zeph.Core.Classes.PlayerQuest currentPlayerQuest = null;

        void Awake() {
        }

        public void Initialise(Zeph.Core.Classes.PlayerQuest playerQuest) {
            currentPlayerQuest = playerQuest;

            var quest = Zeph.Core.Classes.Quest.Read(playerQuest.pq_Quest.q_ID);

            textQuestText.text = quest.q_Name;

            string objectives = "";
            bool first = true;
            foreach (var pqo in playerQuest.PlayerQuestObjectives) {
                if (first) first = false; else objectives += "\r\n";
                var qo = Zeph.Core.Classes.QuestObjective.Read(pqo.pqo_QuestObjective);
                objectives += "• " + qo.qo_Description + " " + pqo.pqo_Progress.ToString() + "/" + qo.qo_Goal.ToString();
            }
            textObjective.text = objectives;
        }

        public void ShowQuest() {
            GeneralOps.ShowQuest(Zeph.Core.Classes.Quest.Read(currentPlayerQuest.pq_Quest.q_ID));
        }
    }
}
