using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDPlayerInfoController : MonoBehaviour {
        HUDPlayerHealthController healthController;
        HUDPlayerNameController nameController;

        void Awake() {
            healthController = GetComponentInChildren<HUDPlayerHealthController>();
            nameController = GetComponentInChildren<HUDPlayerNameController>();
        }

        public void HandleRefresh(Player p) {
            if (nameController != null) nameController.HandleRefresh(p.character.c_Name);
            if (healthController != null) healthController.HandleRefresh(p.characterCombat.combatEntity);
        }
    }
}
