using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class NPC : Character {
        protected NPCLocomotion npcLocomotion;

        /// <summary>
        /// The combat module for this NPC.
        /// </summary>
        /// <remarks>
        /// Since I am using a different combat class to handle NPC combat than just standard character combat,
        /// I have to override the Characters <see cref="Character.characterCombat"/> property and return the <see cref="npcCombat"/> 
        /// casted as a <see cref="CharacterCombat"/> class. 
        /// 
        /// In the <see cref="Awake"/> method also, the <see cref="base.Awake"/> must be
        /// called after npcCombat has been assigned, as the Character adds the Combat component in its Awake method
        /// if there's not one currently attached to the character.
        /// </remarks>
        public NPCCombat npcCombat;
        public override CharacterCombat characterCombat { get {
                return (CharacterCombat)npcCombat;
            }
        }

        public bool isPerformingAction;

        [Header("AI Settings")]
        public float detectionRadius = 5;
        //The higher, and lower, respectively these angles are, the greater detection field of view (basically like eye sight, peripheral vision)
        public float minimumDetectionAngle = -50f;
        public float maximumDetectionAngle = 50f;
        public LayerMask detectionLayer;


        // Start is called before the first frame update
        protected override void Awake() {
            npcLocomotion = GetComponent<NPCLocomotion>();
            npcCombat = GetComponent<NPCCombat>();

            base.Awake();
        }

        protected void FixedUpdate() {
            HandleCurrentAction();
        }

        void LateUpdate() {
            isInteracting = animatorManager.animator.GetBool("IsInteracting");
            animatorManager.animator.SetBool("IsGrounded", false);
        }

        private void HandleCurrentAction() {
            if (currentTarget == null) {
                npcLocomotion.HandleDetection();
            } else {
                npcLocomotion.HandleMoveToTarget();
                npcCombat.HandleAttacking(npcLocomotion.distanceFromTarget);
            }
        }
    }
}
