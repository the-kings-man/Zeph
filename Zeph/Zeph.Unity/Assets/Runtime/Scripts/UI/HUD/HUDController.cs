using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDController : MonoBehaviour {
        public Player player;

        HUDPlayerInfoController playerInfoController;
        HUDTargetInfoController targetInfoController;
        HUDCastingBar castingBar;
        HUDPlayerQuestCanvas questList;

        bool questListIsRefreshed = false;

        // Start is called before the first frame update
        void Awake() {
            playerInfoController = GetComponentInChildren<HUDPlayerInfoController>();
            targetInfoController = GetComponentInChildren<HUDTargetInfoController>();
            castingBar = GetComponentInChildren<HUDCastingBar>();
            questList = GetComponentInChildren<HUDPlayerQuestCanvas>();

            if (player == null) {
                Debug.Log("Must apply a player to the HUDController");
            } else {
                player.OnEntitySelected += Player_OnEntitySelected;

                if (player.currentTarget != null) {
                    targetInfoController.gameObject.SetActive(true);
                } else {
                    targetInfoController.gameObject.SetActive(false);
                }
            }

            castingBar.gameObject.SetActive(false);
        }

        private void Player_OnEntitySelected(object sender, EntitySelectedEventArgs e) {
            if (e.EntityTarget == null) {
                targetInfoController.gameObject.SetActive(false);
            } else {
                targetInfoController.gameObject.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update() {
            if (playerInfoController && player) playerInfoController.HandleRefresh(player);
            if (targetInfoController && targetInfoController.gameObject.activeSelf) {
                if (player.currentTarget.type == EntityType.Character) targetInfoController.HandleRefresh((Character)(player.currentTarget));
            }
            if (castingBar && player) {
                var combatEntity = player.characterCombat.combatEntity;
                if (combatEntity != null && combatEntity.isCasting && combatEntity.currentCastingAttack != null) {
                    castingBar.gameObject.SetActive(true);
                    var target = (long)combatEntity.currentCastingAttack.a_PreparationDuration;
                    var progress = target - (long)combatEntity.castingTimeLeft;
                    castingBar.HandleRefresh(progress, target);
                } else {
                    castingBar.gameObject.SetActive(false);
                }
            }

            if (!questListIsRefreshed) {
                questList.RefreshQuestList();

                questListIsRefreshed = true;
            }
        }
    }
}
