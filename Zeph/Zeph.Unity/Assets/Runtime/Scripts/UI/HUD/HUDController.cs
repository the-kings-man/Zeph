using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDController : MonoBehaviour {
        public Player player;

        HUDPlayerInfoController playerInfoController;

        // Start is called before the first frame update
        void Awake() {
            playerInfoController = GetComponentInChildren<HUDPlayerInfoController>();
        }

        // Update is called once per frame
        void Update() {
            if (playerInfoController) playerInfoController.HandleRefresh(player);
        }
    }
}
