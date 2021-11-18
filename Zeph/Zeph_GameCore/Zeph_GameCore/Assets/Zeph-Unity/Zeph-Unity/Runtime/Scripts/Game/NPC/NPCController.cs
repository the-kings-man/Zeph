using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zeph.Unity {
    public class NPCController : MonoBehaviour {
        /// <summary>
        /// The GUID of the <see cref="ZephGame.Core.Classes.NPC"/> this object relates to
        /// </summary>
        public int npc_ID = -1;

        public float maxForwardSpeed = 8f;        // How fast Ellen can run.
        public float gravity = 20f;               // How fast Ellen accelerates downwards when airborne.
        public float jumpSpeed = 10f;             // How fast Ellen takes off when jumping.
        public float minTurnSpeed = 400f;         // How fast Ellen turns when moving at maximum speed.
        public float maxTurnSpeed = 1200f;        // How fast Ellen turns when stationary.
        public float idleTimeout = 5f;            // How long before Ellen starts considering random idles.
        public bool canAttack;                    // Whether or not Ellen can swing her staff.


        protected AnimatorStateInfo m_CurrentStateInfo;    // Information about the base layer of the animator cached.
        protected AnimatorStateInfo m_NextStateInfo;
        protected bool m_IsAnimatorTransitioning;
        protected AnimatorStateInfo m_PreviousCurrentStateInfo;    // Information about the base layer of the animator from last frame.
        protected AnimatorStateInfo m_PreviousNextStateInfo;
        protected bool m_PreviousIsAnimatorTransitioning;
        protected bool m_IsGrounded = true;            // Whether or not Ellen is currently standing on the ground.
        protected bool m_PreviouslyGrounded = true;    // Whether or not Ellen was standing on the ground last frame.
        protected bool m_ReadyToJump;                  // Whether or not the input state and Ellen are correct to allow jumping.
        protected float m_DesiredForwardSpeed;         // How fast Ellen aims be going along the ground based on input.
        protected float m_ForwardSpeed;                // How fast Ellen is currently going along the ground.
        protected float m_VerticalSpeed;               // How fast Ellen is currently moving up or down.
        //protected PlayerInput m_Input;                 // Reference used to determine how Ellen should move.
        protected CharacterController m_CharCtrl;      // Reference used to actually move Ellen.
        //protected Animator m_Animator;                 // Reference used to make decisions based on Ellen's current animation and to set parameters.
        protected Material m_CurrentWalkingSurface;    // Reference used to make decisions about audio.
        protected Quaternion m_TargetRotation;         // What rotation Ellen is aiming to have based on input.
        protected float m_AngleDiff;                   // Angle in degrees between Ellen's current rotation and her target rotation.
        //protected Collider[] m_OverlapResult = new Collider[8];    // Used to cache colliders that are near Ellen.
        //protected bool m_InAttack;                     // Whether Ellen is currently in the middle of a melee attack.
        //protected bool m_InCombo;                      // Whether Ellen is currently in the middle of her melee combo.
        //protected Damageable m_Damageable;             // Reference used to set invulnerablity and health based on respawning.
        protected Renderer[] m_Renderers;              // References used to make sure Renderers are reset properly. 
        //protected Checkpoint m_CurrentCheckpoint;      // Reference used to reset Ellen to the correct position on respawn.
        //protected bool m_Respawning;                   // Whether Ellen is currently respawning.
        protected float m_IdleTimer;                   // Used to count up to Ellen considering a random idle.

        const float k_AirborneTurnSpeedProportion = 5.4f;
        const float k_GroundedRayDistance = 1f;
        const float k_JumpAbortSpeed = 10f;
        const float k_MinEnemyDotCoeff = 0.2f;
        const float k_InverseOneEighty = 1f / 180f;
        const float k_StickingGravityProportion = 0.3f;
        const float k_GroundAcceleration = 20f;
        const float k_GroundDeceleration = 25f;

        protected Zeph.Core.Combat.CombatEntity m_combatEntity;

        // Called automatically by Unity when the script first exists in the scene.
        void Awake() {
            m_CharCtrl = GetComponent<CharacterController>();

            //meleeWeapon.SetOwner(gameObject);

            //s_Instance = this;
        }

        // Called automatically by Unity once every Physics step.
        void FixedUpdate() {

            CalculateForwardMovement();
            CalculateVerticalMovement();

            m_PreviouslyGrounded = m_IsGrounded;


            Vector3 movement = Vector3.zero;

            // If Ellen is on the ground...
            if (m_IsGrounded) {
                // ... raycast into the ground...
                RaycastHit hit;
                Ray ray = new Ray(transform.position + Vector3.up * k_GroundedRayDistance * 0.5f, -Vector3.up);
                if (Physics.Raycast(ray, out hit, k_GroundedRayDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore)) {
                    // ... and get the movement of the root motion rotated to lie along the plane of the ground.
                    //movement = Vector3.ProjectOnPlane(m_Animator.deltaPosition, hit.normal);

                    //// Also store the current walking surface so the correct audio is played.
                    Renderer groundRenderer = hit.collider.GetComponentInChildren<Renderer>();
                    m_CurrentWalkingSurface = groundRenderer ? groundRenderer.sharedMaterial : null;
                } else {
                    // If no ground is hit just get the movement as the root motion.
                    // Theoretically this should rarely happen as when grounded the ray should always hit.
                    //movement = m_Animator.deltaPosition;
                    m_CurrentWalkingSurface = null;
                }
            } else {
                // If not grounded the movement is just in the forward direction.
                movement = m_ForwardSpeed * transform.forward * Time.deltaTime;
            }

            // Rotate the transform of the character controller by the animation's root rotation.
            //m_CharCtrl.transform.rotation *= m_Animator.deltaRotation;

            // Add to the movement with the calculated vertical speed.
            movement += m_VerticalSpeed * Vector3.up * Time.deltaTime;

            // Move the character controller.
            m_CharCtrl.Move(movement);

            // After the movement store whether or not the character controller is grounded.
            m_IsGrounded = m_CharCtrl.isGrounded;

            //// If Ellen is not on the ground then send the vertical speed to the animator.
            //// This is so the vertical speed is kept when landing so the correct landing animation is played.
            //if (!m_IsGrounded)
            //    m_Animator.SetFloat(m_HashAirborneVerticalSpeed, m_VerticalSpeed);

            //// Send whether or not Ellen is on the ground to the animator.
            //m_Animator.SetBool(m_HashGrounded, m_IsGrounded);
        }

        // Called each physics step.
        void CalculateForwardMovement() {

        }

        // Called each physics step.
        void CalculateVerticalMovement() {
            if (m_IsGrounded) {
                // When grounded we apply a slight negative vertical speed to make Ellen "stick" to the ground.
                m_VerticalSpeed = -gravity * k_StickingGravityProportion;

                // If jump is held, Ellen is ready to jump and not currently in the middle of a melee combo...
                if (m_ReadyToJump/* && !m_InCombo*/) {
                    // ... then override the previously set vertical speed and make sure she cannot jump again.
                    m_VerticalSpeed = jumpSpeed;
                    m_IsGrounded = false;
                    m_ReadyToJump = false;
                }
            } else {
                // If a jump is approximately peaking, make it absolute.
                if (Mathf.Approximately(m_VerticalSpeed, 0f)) {
                    m_VerticalSpeed = 0f;
                }

                // If Ellen is airborne, apply gravity.
                m_VerticalSpeed -= gravity * Time.deltaTime;
            }
        }

        public void Jump() {
            m_ReadyToJump = true;
        }

        public void Interact() {
            if (npc_ID != -1) {
                var combatSystem = (Zeph.Core.Combat.CombatSystem)Zeph.Core.SystemLocator.GetService<Zeph.Core.Combat.ICombatSystem>();
                var npc = Zeph.Core.Classes.NPC.Read(npc_ID);
                var rep = combatSystem.GetPlayerReputationWithNPC(GeneralOps.CurrentPlayer, npc);

                if (rep == 0) {
                    //combat
                    if (m_combatEntity == null) {
                        m_combatEntity = combatSystem.GenerateCombatEntity(npc.Character);
                    }

                    var playerCombatEntity = GeneralOps.PlayerCombatEntity;

                    var res = combatSystem.CalculateDamage(playerCombatEntity, m_combatEntity, Zeph.Core.Classes.Attack.Read(1));
                    m_combatEntity.currentHealth -= res.damage;

                    if (m_combatEntity.currentHealth <= 0) {
                        Destroy(this.gameObject);
                        combatSystem.NPCDied(npc, new Zeph.Core.Combat.DeathReason() {
                            source = Zeph.Core.Combat.DeathSource.Player,
                            player = GeneralOps.CurrentPlayer
                        });
                    }
                } else {
                    //var player = ZephGame.GeneralOps.CurrentPlayer;
                    //var db = Zeph.Core.GeneralOps.GetDatabaseConnection();

                    //var lstDialog = Zeph.Core.Dialog.DialogSystem.CurrentDialogForNPC(npc, player);
                    //if (lstDialog != null && lstDialog.Count > 0) {
                    //    ZephGame.GeneralOps.StartDialog(lstDialog[0]);
                    //}
                }
            }
        }

        // Called each physics step (so long as the Animator component is set to Animate Physics) after FixedUpdate to override root motion.
        //void OnAnimatorMove() {
        //    Vector3 movement;

        //    // If Ellen is on the ground...
        //    if (m_IsGrounded) {
        //        // ... raycast into the ground...
        //        RaycastHit hit;
        //        Ray ray = new Ray(transform.position + Vector3.up * k_GroundedRayDistance * 0.5f, -Vector3.up);
        //        if (Physics.Raycast(ray, out hit, k_GroundedRayDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore)) {
        //            // ... and get the movement of the root motion rotated to lie along the plane of the ground.
        //            movement = Vector3.ProjectOnPlane(m_Animator.deltaPosition, hit.normal);

        //            //// Also store the current walking surface so the correct audio is played.
        //            Renderer groundRenderer = hit.collider.GetComponentInChildren<Renderer>();
        //            m_CurrentWalkingSurface = groundRenderer ? groundRenderer.sharedMaterial : null;
        //        } else {
        //            // If no ground is hit just get the movement as the root motion.
        //            // Theoretically this should rarely happen as when grounded the ray should always hit.
        //            movement = m_Animator.deltaPosition;
        //            m_CurrentWalkingSurface = null;
        //        }
        //    } else {
        //        // If not grounded the movement is just in the forward direction.
        //        movement = m_ForwardSpeed * transform.forward * Time.deltaTime;
        //    }

        //    // Rotate the transform of the character controller by the animation's root rotation.
        //    m_CharCtrl.transform.rotation *= m_Animator.deltaRotation;

        //    // Add to the movement with the calculated vertical speed.
        //    movement += m_VerticalSpeed * Vector3.up * Time.deltaTime;

        //    // Move the character controller.
        //    m_CharCtrl.Move(movement);

        //    // After the movement store whether or not the character controller is grounded.
        //    m_IsGrounded = m_CharCtrl.isGrounded;

        //    //// If Ellen is not on the ground then send the vertical speed to the animator.
        //    //// This is so the vertical speed is kept when landing so the correct landing animation is played.
        //    //if (!m_IsGrounded)
        //    //    m_Animator.SetFloat(m_HashAirborneVerticalSpeed, m_VerticalSpeed);

        //    //// Send whether or not Ellen is on the ground to the animator.
        //    //m_Animator.SetBool(m_HashGrounded, m_IsGrounded);
        //}
    }
}
