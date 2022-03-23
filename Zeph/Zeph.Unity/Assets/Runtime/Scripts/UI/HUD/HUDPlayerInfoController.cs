﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDPlayerInfoController : MonoBehaviour {
        HUDStatBar healthController;
        HUDPlayerNameController nameController;

        void Awake() {
            healthController = GetComponentInChildren<HUDStatBar>();
            nameController = GetComponentInChildren<HUDPlayerNameController>();
        }

        public void HandleRefresh(Player p) {
            if (nameController != null) nameController.HandleRefresh(p.character.c_Name);
            if (healthController != null) {
                if (p.characterCombat.combatEntity != null) {
                    healthController.HandleRefresh(true, p.characterCombat.combatEntity.currentHealth, p.characterCombat.combatEntity.maxHealth);
                } else {
                    healthController.HandleRefresh(false, 0, 0);
                }
            }
        }
    }
}