using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class InputManager : MonoBehaviour {
        public Vector2 movementInput;
        public Vector2 cameraInput;

        public float cameraInputHorizontal;
        public float cameraInputVertical;
        public bool cameraInputFreeLook = false;
        public float cameraInputRotate = 0f;
        public float cameraMoveInOut = 0f;

        public float moveAmount;
        public float verticalInput;
        public float horizontalInput;

        public bool sprintInput;
        public bool jumpInput;

        PlayerControls playerControls;
        PlayerLocomotion playerLocomotion;
        AnimatorManager animatorManager;
        Player player;
        public HotbarController hotbarController;

        void Awake() {
            animatorManager = GetComponent<AnimatorManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            player = GetComponent<Player>();
        }

        private void OnEnable() {
            if (playerControls == null) {
                playerControls = new PlayerControls();

                //Player moving
                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

                playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

                playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
                playerControls.PlayerActions.Jump.canceled += i => jumpInput = false;

                //Camera
                playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerMovement.CameraFreeLook.performed += i => cameraInputFreeLook = true;
                playerControls.PlayerMovement.CameraFreeLook.canceled += i => cameraInputFreeLook = false;
                playerControls.PlayerMovement.CameraRotateLeft.performed += i => cameraInputRotate -= 1;
                playerControls.PlayerMovement.CameraRotateLeft.canceled += i => cameraInputRotate += 1;
                playerControls.PlayerMovement.CameraRotateRight.performed += i => cameraInputRotate += 1;
                playerControls.PlayerMovement.CameraRotateRight.canceled += i => cameraInputRotate -= 1;

                playerControls.PlayerMovement.CameraMoveInOut.performed += i => cameraMoveInOut = i.ReadValue<float>();


                playerControls.PlayerActions.Select.performed += (i) => {
                    RaycastHit raycastHit;
                    Ray ray = Camera.main.ScreenPointToRay(new Vector3(UnityEngine.InputSystem.Mouse.current.position.x.ReadValue(), UnityEngine.InputSystem.Mouse.current.position.y.ReadValue()));
                    if (Physics.Raycast(ray, out raycastHit, 100f)) {
                        if (raycastHit.transform != null) {
                            var entity = (Entity)(raycastHit.transform.gameObject.GetComponent<Entity>());
                            if (entity != null) {
                                player.EntitySelected(entity);
                            } else {
                                player.EntitySelected(null);
                            }
                        }
                    }
                }; //mouse up?

                playerControls.PlayerHotbars.Button1.canceled += (i) => {
                    if (hotbarController != null) hotbarController.ButtonPressed(1);
                };

                playerControls.PlayerHotbars.Button2.canceled += (i) => {
                    if (hotbarController != null) hotbarController.ButtonPressed(2);
                };

                playerControls.PlayerHotbars.Button3.canceled += (i) => {
                    if (hotbarController != null) hotbarController.ButtonPressed(3);
                };

                playerControls.PlayerHotbars.Button4.canceled += (i) => {
                    if (hotbarController != null) hotbarController.ButtonPressed(4);
                };
            }

            playerControls.Enable();
        }

        private void OnDisable() {
            playerControls.Disable();
        }

        public void HandleAllInputs() {
            HandleMovementInput();
            HandleSprintingInput();
            HandeJumpingInput();
            //handle action input
        }

        private void HandleMovementInput() {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            cameraInputVertical = cameraInput.y;
            cameraInputHorizontal = cameraInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); //abs cos animator can't work with negative value
            animatorManager.UpdateAnimatorValues(0, moveAmount, sprintInput);
        }

        private void HandleSprintingInput() {
            if (sprintInput && moveAmount > 0.5f) {
                playerLocomotion.isSprinting = true;
            } else {
                playerLocomotion.isSprinting = false;
            }
        }

        private void HandeJumpingInput() {
            if (jumpInput) {
                jumpInput = false;
                playerLocomotion.DoJump();
            }
        }
    }
}