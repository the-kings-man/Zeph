﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Player : Character {

        public CameraManager cameraManager;

        Animator animator;
        InputManager inputManager;
        PlayerLocomotion playerLocomotion;

        // Start is called before the first frame update
        void Awake() {
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
    }
}