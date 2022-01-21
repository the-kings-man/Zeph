using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDTargetInfoController : MonoBehaviour {
        HUDStatBar healthController;
        HUDPlayerNameController nameController;

        void Awake() {
            healthController = GetComponentInChildren<HUDStatBar>();
            nameController = GetComponentInChildren<HUDPlayerNameController>();
        }

        public void HandleRefresh(Character c) {
            if (nameController != null) nameController.HandleRefresh(c.character.c_Name);
            if (healthController != null) {
                if (c.characterCombat.combatEntity != null) {
                    healthController.HandleRefresh(true, c.characterCombat.combatEntity.currentHealth, c.characterCombat.combatEntity.maxHealth);
                } else {
                    healthController.HandleRefresh(false, 0, 0);
                }
            }
        }
    }
}
