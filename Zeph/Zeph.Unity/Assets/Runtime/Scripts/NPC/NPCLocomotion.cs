using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Zeph.Unity {
    public class NPCLocomotion : CharacterLocomotion {

        //AnimatorManager animatorManager;
        //InputManager inputManager;
        NPC npc;
        NavMeshAgent navMeshAgent;
        //Rigidbody rigidbody;
        //Vector3 moveDirection;

        //public Transform cameraObject;

        //[Header("Vertical Movements")]
        //public float inAirTimer;
        //public float leapingSpeed;
        //public float fallingSpeed;
        //public LayerMask groundLayer;
        //public float rayCastHeightOffset = 0.5f;
        //public float jumpHeight = 3;
        //public float gravityIntensity = -15;
        //public float maxClimbingHeight = 0.1f;

        //[Header("Horizontal Movements")]
        //public float walkingSpeed = 1.5f;
        //public float runningSpeed = 5f;
        //public float sprintingSpeed = 7f;
        //public float rotationSpeed = 15f;

        //[Header("Movement Flags")]
        //public bool isSprinting;
        //public bool isGrounded;
        //public bool isJumping;

        public float distanceFromTarget;
        public float stoppingDistance = 1f;

        protected new void Awake() {
            base.Awake();
            //animatorManager = GetComponent<AnimatorManager>();
            //inputManager = GetComponent<InputManager>();
            npc = GetComponent<NPC>();
            //rigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        }
        
        void Start() {
            navMeshAgent.enabled = false;
            characterRigidbody.isKinematic = false;
        }

        //public void HandleAllMovement() {
        //    HandleFallingAndLanding();

        //    if (!player.isInteracting) {
        //        HandleMovement();
        //        HandleRotation();
        //    }
        //}

        //protected override void HandleMovement() {
        //    base.HandleMovement();
        //    if (!isJumping) {
        //        moveDirection = cameraObject.forward * inputManager.verticalInput;
        //        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        //        moveDirection.Normalize();
        //        moveDirection.y = 0;

        //        var moveSpeed = 0f;
        //        if (isSprinting) {
        //            moveSpeed = sprintingSpeed;
        //        } else {
        //            if (inputManager.moveAmount >= 0.5f) {
        //                moveSpeed = runningSpeed;
        //            } else {
        //                moveSpeed = walkingSpeed;
        //            }
        //        }

        //        Vector3 movementVelocity = moveDirection * moveSpeed;
        //        rigidbody.velocity = movementVelocity;
        //    }
        //}

        //protected override void HandleRotation() {
        //    if (!isJumping) {
        //        Vector3 targetDirection = Vector3.zero;

        //        targetDirection = cameraObject.forward * inputManager.verticalInput;
        //        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        //        targetDirection.Normalize();
        //        targetDirection.y = 0;

        //        if (targetDirection == Vector3.zero)
        //            targetDirection = transform.forward;

        //        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        //        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //rotation between point a and point b

        //        transform.rotation = playerRotation;
        //    }
        //}

        //protected override void HandleFallingAndLanding() {
        //    RaycastHit hit;
        //    Vector3 rayCastOrigin = transform.position;
        //    Vector3 groundedPosition = transform.position;
        //    rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        //    if (!isGrounded && !isJumping) {
        //        if (!player.isInteracting) {
        //            animatorManager.PlayTargetAnimation("Falling", true);
        //        }

        //        inAirTimer = inAirTimer + Time.deltaTime;
        //        //rigidbody.AddForce(transform.forward * leapingSpeed);
        //        rigidbody.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
        //    }

        //    if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer)) {
        //        if (!isGrounded && !player.isInteracting) {
        //            animatorManager.PlayTargetAnimation("Land", true);
        //        }

        //        Vector3 rayCastHitPoint = hit.point; //save point that the raycast hit
        //        groundedPosition.y = rayCastHitPoint.y;
        //        inAirTimer = 0;
        //        isGrounded = true;
        //    } else {
        //        isGrounded = false;
        //    }

        //    //adjust characters model based upon if we are grounded or not -> bring in contact with the ground. Had to uncheck "use gravity" in rigidbody
        //    if (isGrounded && !isJumping) {
        //        if ((groundedPosition.y - transform.position.y) < maxClimbingHeight) {
        //            if (player.isInteracting || inputManager.moveAmount > 0) {
        //                transform.position = Vector3.Lerp(transform.position, groundedPosition, Time.deltaTime / 0.1f);
        //            } else {
        //                transform.position = groundedPosition;
        //            }
        //        }

        //    }
        //}

        public void HandleDetection() {
            Collider[] colliders = Physics.OverlapSphere(transform.position, npc.detectionRadius, npc.detectionLayer);

            for (int i = 0; i < colliders.Length; i++) {
                var characterCollidedWith = colliders[i].transform.GetComponent<Character>();

                if (characterCollidedWith != null && characterCollidedWith != npc) {
                    //TODO: Check for reputation/aggro
                    Vector3 targetDirection = characterCollidedWith.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > npc.minimumDetectionAngle && viewableAngle < npc.maximumDetectionAngle) {
                        npc.currentTarget = characterCollidedWith;
                    }
                }
            }
        }

        public void HandleMoveToTarget() {
            Vector3 targetDirection = npc.currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(npc.currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            //if we are performing an action, stop our movement
            if (npc.isPerformingAction) {
                npc.animatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navMeshAgent.enabled = false;
            } else {
                if (distanceFromTarget > stoppingDistance) {
                    animatorManager.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                } else if (distanceFromTarget <= stoppingDistance) {
                    animatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            HandleRotateTowardsTarget();

            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;
        }

        //a hybrid of manual and navmesh movements
        private void HandleRotateTowardsTarget() {
            //rotate manually
            if (npc.isPerformingAction) {
                Vector3 direction = npc.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero) {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            } else { //rotate with pathfinding (NavMeshAgent)
                Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = characterRigidbody.velocity;

                navMeshAgent.enabled = true;
                navMeshAgent.SetDestination(npc.currentTarget.transform.position);
                characterRigidbody.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
            }
        }
    }
}