using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class NPC : Character {
        protected NPCLocomotion npcLocomotion;
        protected NPCCombat npcCombat;

        public bool isPerformingAction;

        [Header("AI Settings")]
        public float detectionRadius = 5;
        //The higher, and lower, respectively these angles are, the greater detection field of view (basically like eye sight, peripheral vision)
        public float minimumDetectionAngle = -50f;
        public float maximumDetectionAngle = 50f;
        public LayerMask detectionLayer;


        // Start is called before the first frame update
        protected override void Awake() {
            base.Awake();

            npcLocomotion = GetComponent<NPCLocomotion>();
            npcCombat = GetComponent<NPCCombat>();
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
