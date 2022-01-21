using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Player : Character {

        public CameraManager cameraManager;

        InputManager inputManager;
        PlayerLocomotion playerLocomotion;

        // Start is called before the first frame update
        protected override void Awake() {
            base.Awake();

            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        protected void Update() {
            inputManager.HandleAllInputs();
        }

        void FixedUpdate() {
            playerLocomotion.HandleAllMovement();
        }

        void LateUpdate() {
            cameraManager.HandleAllCameraMovement();

            isInteracting = animatorManager.animator.GetBool("IsInteracting");
            playerLocomotion.isJumping = animatorManager.animator.GetBool("IsJumping");
            animatorManager.animator.SetBool("IsGrounded", playerLocomotion.isGrounded);
        }

        public void PerformAttack(Zeph.Core.Classes.Attack attackToPerform) {
            if (currentTarget.type == EntityType.Character) {
                var attackResult = characterCombat.Attack(((Character)currentTarget).characterCombat, attackToPerform);

                if (!attackResult.success) {
                    Debug.Log("Attack failed due to: " + attackResult.reason.ToString());
                }
            }
        }
    }
}