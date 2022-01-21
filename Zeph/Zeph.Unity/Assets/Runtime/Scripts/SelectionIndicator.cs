using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class SelectionIndicator : MonoBehaviour {
        public Transform currentTarget;
        public Player player;

        void Awake() {
            if (player == null) {
                Debug.Log("Must apply a player to the SelectionIndicator");
            } else {
                player.OnEntitySelected += Player_OnEntitySelected;

                if (player.currentTarget != null) {
                    gameObject.SetActive(true);
                } else {
                    gameObject.SetActive(false);
                }
            }
        }

        private void Player_OnEntitySelected(object sender, EntitySelectedEventArgs e) {
            if (e.EntityTarget != null) {
                currentTarget = e.EntityTarget.gameObject.transform;
                gameObject.SetActive(true);
            } else {
                currentTarget = null;
                gameObject.SetActive(false);
            }
        }

        void LateUpdate() {
            if (currentTarget) {
                this.transform.position = currentTarget.position;
            }
        }
    }

}
