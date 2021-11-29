using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zeph.Core.Combat;

namespace Zeph.Unity {
    /// <summary>
    /// A base combat class.
    /// </summary>
    /// <remarks>
    /// Will handle things like basic combat variables, combat state, attack cooldowns and global cooldown, current attack, database link.
    /// 
    /// combatState
    ///  - in attack
    ///  - global cooldown
    ///  - idle
    ///  - casting
    ///  - out of combat
    /// Zeph.Core.CombatEntity
    ///   + attack cooldown times, global cooldown time
    /// 
    /// The class which handles the interactions between the other character components and the low-level Zeph.Core.CombatEntity. The combat entity is what handles casting and cooldowns and damage application,
    /// this just helps with animation, locomotion, creating projectiles and providing a top level interface to perform attacks/be attacked.
    /// 
    /// </remarks>
    public class CharacterCombat : MonoBehaviour
    {
        public CombatEntity combatEntity { get; private set; }

        Character character;
        AnimatorManager animatorManager;

        virtual protected void Awake() {
            character = GetComponent<Character>();
            animatorManager = GetComponent<AnimatorManager>();
        }

        virtual protected void Update() {
            combatEntity?.Update(Time.deltaTime * 1000f); //multiply by 1000 cos zeph framework is in milliseconds
        }

        bool justPunched = true;
        public CharacterCombatAttackResult Attack(CharacterCombat characterToAttack, Zeph.Core.Classes.Attack attackBeingPerformed) {
            //TODO: This here should rather start combat when combat is entered, i.e. the player attacks an enemy or the enemy attacks the player. This will help reduce processing power. But seeing as this is not a thing atm, let's just start combat here
            if (combatEntity == null) this.StartCombat();
            if (characterToAttack.combatEntity == null) characterToAttack.StartCombat();

            if (combatEntity == null) {
                throw new Zeph.Core.Classes.ExceptionHandling.GeneralException("Zeph.Unity.CharacterCombat", 1, "Must call StartCombat to generate a CombatEntity before calling Attack.");
            }

            var res = new CharacterCombatAttackResult();

            if (Vector3.Distance(this.gameObject.transform.position, characterToAttack.gameObject.transform.position) > attackBeingPerformed.a_Distance) {
                res.success = false;
                res.reason = CharacterCombatAttackResultFailReason.TooFar;
            } else if (characterToAttack == this) {
                res.success = false;
                res.reason = CharacterCombatAttackResultFailReason.SameCharacter;
            } else {
                var attackResult = combatEntity.PerformAttack(characterToAttack.combatEntity, attackBeingPerformed);

                if (attackResult.success) {
                    //TODO: play the animation, create the projectile if needed, move the player etc...
                    res.success = true;

                    if (justPunched) {
                        animatorManager.PlayTargetAnimationClip("Attack", "Punching", "Punching", true);
                    } else {
                        animatorManager.PlayTargetAnimationClip("Attack", "Punching", "Headbutt", true);
                    }
                    justPunched = !justPunched;

                } else {
                    res.success = false;
                    res.reason = CharacterCombatAttackResultFailReason.CombatEntityFail;
                    res.combatEntityResult = attackResult;
                }
            }

            return res;
        }

        public void StartCombat() {
            var combatSystem = Zeph.Core.SystemLocator.GetService<ICombatSystem>();

            combatEntity = combatSystem.GenerateCombatEntity(character.character);
            combatEntity.OnDeath += (s, e) => {
                //this.gameObject.SetActive(false);
                animatorManager.PlayTargetAnimation("Dying", true);
                Debug.Log(this.ToString() + " died :(");
            };
            combatEntity.OnTakeDamage += (s, e) => {
                GameController.Instance.CreateDamageIndicator(e.Result.damage.ToString(), this.gameObject.transform.position);
                Debug.Log(this.ToString() + " took " + e.Result.damage.ToString() + " damage.");
            };
        }

        public void LeaveCombat() {
            combatEntity = null;
        }
    }

    public class CharacterCombatAttackResult {
        public bool success = false;
        public CharacterCombatAttackResultFailReason reason = CharacterCombatAttackResultFailReason.None;
        public Zeph.Core.Combat.AttackResult combatEntityResult = null;
    }

    public enum CharacterCombatAttackResultFailReason {
        None = 1,
        TooFar = 2,
        CombatEntityFail = 3,
        SameCharacter = 4
    }
}