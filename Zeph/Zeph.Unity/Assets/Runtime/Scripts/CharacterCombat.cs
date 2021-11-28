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
        public CombatEntity combatEntity;

        public CharacterCombatAttackResult Attack(CharacterCombat characterToAttack, Zeph.Core.Classes.Attack attackBeingPerformed) {
            var res = new CharacterCombatAttackResult();

            if (combatEntity.combatState == CombatState.Idle) {
                if (combatEntity.cooldowns.ContainsKey(attackBeingPerformed.a_ID)) {
                    res.success = false;
                    res.reason = CharacterCombatAttackResultFailReason.InCooldown;
                } else {
                    PerformAttack(characterToAttack, attackBeingPerformed);
                    res.success = true;
                }
            } else {
                switch (combatEntity.combatState) {
                    case CombatState.Attacking:
                        res.success = false;
                        res.reason = CharacterCombatAttackResultFailReason.Attacking;
                        break;
                    case CombatState.Casting:
                        res.success = false;
                        res.reason = CharacterCombatAttackResultFailReason.Casting;
                        break;
                    case CombatState.GlobalCooldown:
                        res.success = false;
                        res.reason = CharacterCombatAttackResultFailReason.InGlobalCooldown;
                        break;
                    case CombatState.OutOfCombat:
                        res.success = false;
                        res.reason = CharacterCombatAttackResultFailReason.OutOfCombat;
                        break;
                }
            }
            return res;
        }

        virtual protected void PerformAttack(CharacterCombat characterToAttack, Zeph.Core.Classes.Attack attackBeingPerformed) {
            if (attackBeingPerformed.a_AttackType == Core.Enums.AttackType.Strike) {
                if (attackBeingPerformed.a_PreparationDuration )
                characterToAttack.GetAttacked(this, attackBeingPerformed);
                combatEntity.PerformAttack(attackBeingPerformed);
            } else {
                throw new System.NotImplementedException();
            }
        }

        virtual protected void GetAttacked(CharacterCombat characterAttackIsFrom, Zeph.Core.Classes.Attack attackBeingPerformed) {

        }
    }

    public class CharacterCombatAttackResult {
        public bool success = false;
        public CharacterCombatAttackResultFailReason reason = CharacterCombatAttackResultFailReason.None;
    }

    public enum CharacterCombatAttackResultFailReason {
        None = 1,
        InCooldown = 2,
        InGlobalCooldown = 3,
        Casting = 4,
        OutOfCombat = 5,
        Attacking = 6
    }
}