using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDHotbar : MonoBehaviour {
        public List<string> lstButtonKeys;
        public List<int> lstButtonAttacks;
        public GameObject buttonTemplate;

        public float buttonWidth = 100f;
        public bool isHorizontal = true;

        List<HUDHotbarButton> lstButtons = new List<HUDHotbarButton>();

        // Start is called before the first frame update
        void Awake() {
            if (lstButtonKeys.Count != lstButtonAttacks.Count) {
                Debug.Log("Number of button keys does not match number of button attacks.");
            } else {
                float x = 0;
                int numButtons = lstButtonKeys.Count;
                x = -buttonWidth * numButtons / 2;

                for (var i = 0; i < lstButtonKeys.Count; i++) {
                    var button = Instantiate(buttonTemplate, this.transform);

                    var hotbarButton = button.GetComponent<HUDHotbarButton>();
                    hotbarButton.HandleButtonRefresh(lstButtonKeys[i], lstButtonAttacks[i]);

                    if (isHorizontal) {
                        button.transform.Translate(x, 0, 0);
                    } else {
                        button.transform.Translate(0, x, 0);
                    }
                    x += buttonWidth;

                    lstButtons.Add(hotbarButton);
                }
            }
        }

        public void HandleCooldownRefresh(Zeph.Core.Combat.CombatEntity combatEntity) {
            if (lstButtons.Count > 0) {
                foreach (var button in lstButtons) button.HandleCooldownRefresh(combatEntity);
            }
        }

        public HUDHotbarButton GetButtonForKey(string key) {
            foreach (var button in lstButtons) {
                if (button.key == key) {
                    return button;
                }
            }
            return null;
        }

    }
}
