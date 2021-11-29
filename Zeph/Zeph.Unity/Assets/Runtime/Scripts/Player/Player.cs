using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Player : Character {

        public CameraManager cameraManager;
        public SelectionIndicator selectionIndicator;

        Animator animator;
        InputManager inputManager;
        PlayerLocomotion playerLocomotion;

        // Start is called before the first frame update
        protected override void Awake() {
            base.Awake();

            animator = GetComponent<Animator>();
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        void Update() {
            inputManager.HandleAllInputs();
        }

        void FixedUpdate() {
            playerLocomotion.HandleAllMovement();
        }

        void LateUpdate() {
            cameraManager.HandleAllCameraMovement();

            isInteracting = animator.GetBool("IsInteracting");
            playerLocomotion.isJumping = animator.GetBool("IsJumping");
            animator.SetBool("IsGrounded", playerLocomotion.isGrounded);
        }

        public override void EntitySelected(Entity entity) {
            base.EntitySelected(entity);

            if (selectionIndicator) {
                if (entity != null) {
                    selectionIndicator.currentTarget = entity.gameObject.transform;
                } else {
                    selectionIndicator.currentTarget = null;
                }
            }
        }

        public void PerformAttack(Zeph.Core.Classes.Attack attackToPerform) {
            if (currentTarget.type == EntityType.Character) {
                characterCombat.Attack(((Character)currentTarget).characterCombat, attackToPerform);
            }
        }
    }
}