using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Combat {
    public interface ICombatSystem {
        long CalculateDamage(CombatEntity from, CombatEntity to, Classes.Attack attack);
        long CalculateHealth(Stats stats);
        CombatEntity GenerateCombatEntity(Classes.Character character);
        void CharacterDied(Classes.Character character, DeathReason reason);
        event CharacterDiedEventHandler OnCharacterDeath;
    }

    /// <summary>
    /// The class which handles the complex calculations and setup of combat.
    /// </summary>
    /// <remarks>
    /// 
    /// Current plan being, as you engage in combat, you generate a CombatEntity for each of the entities doing battle,
    /// then as it comes for each one to attack, you call calculate damage and change the target entities health accordingly.
    /// 
    /// Once one of the combat entities health is equal to or less than 0, that combat entity is dead.
    /// 
    /// </remarks>
    public class CombatSystem : ICombatSystem {

        public event CharacterDiedEventHandler OnCharacterDeath;

        #region "Scripts"

        string calculateDamageScript = @"
if (attack.a_AttackType == AttackType.Instant) {
    return from.currentStats.s_Strength * attack.a_Damage - to.currentStats.s_Hardness;
} else if (attack.a_AttackType == AttackType.Projectile) {
    return 0;
} else {
    return 0;
}
";
        string calculateHealthScript = @"
return stats.s_Constitution * 100;
";
        #endregion

        #region Calculations

        public long CalculateDamage(CombatEntity from, CombatEntity to, Classes.Attack attack) {
            var statsFrom = from.character.Stats;
            var statsTo = to.character.Stats;

            var engine = new Jint.Engine();

            engine.SetValue("AttackType", TypeReference.CreateTypeReference(engine, typeof(Enums.AttackType)))
                .SetValue("from", from)
                .SetValue("to", to)
                .SetValue("attack", attack);

            return Convert.ToInt64(engine.Evaluate(calculateDamageScript).ToObject());
        }

        public long CalculateHealth(Stats stats) {
            var engine = new Jint.Engine();

            engine.SetValue("stats", stats);

            return Convert.ToInt64(engine.Evaluate(calculateHealthScript).ToObject());
        }

        #endregion

        public void CharacterDied(Character character, DeathReason reason) {
            OnCharacterDeath?.Invoke(null, new CharacterDiedEventArgs() {
                Character = character,
                Reason = reason
            });
        }

        public CombatEntity GenerateCombatEntity(Character character) {
            return new CombatEntity(character);
        }

        /// <summary>
        /// Gets the lowest reputation the player has with this npc. Looks at the npc.character.characterfactions and gets the repuation from player.playerfactions. Returns -1 if it could not be found.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="npc"></param>
        /// <returns></returns>
        public int GetPlayerReputationWithNPC(Classes.Player player, Classes.NPC npc) {
            var lstCharacterFactions = npc.Character.CharacterFactions;
            var lstPlayerFactions = player.PlayerFactions;

            int reputation = int.MaxValue;

            foreach (var cf in lstCharacterFactions) {
                foreach (var pf in lstPlayerFactions) {
                    if (cf.cf_Faction == pf.pf_Faction) {
                        if (reputation > pf.pf_Reputation) {
                            reputation = pf.pf_Reputation;
                            break;
                        }
                    }
                }
            }

            if (reputation == int.MaxValue) {
                return -1;
            } else {
                return reputation;
            }
        }
    }

    /// <summary>
    /// The class which handles the low level side of damage application/cooldowns/stat modifiers/damage over time effects/health/death.
    /// </summary>
    public class CombatEntity {
        public const int GLOBAL_ATTACK_COOLDOWN = 100; //100 milliseconds

        public Classes.Character character;

        public CombatState combatState;

        #region "Stats"

        public List<CombatStatModifier> combatStatModifiers;

        public long maxHealth;
        public Stats baseStats;

        public long currentHealth;
        public Stats currentStats;

        #endregion

        #region "Cooldowns"

        public System.Collections.Concurrent.ConcurrentDictionary<int, AttackCooldown> cooldowns = new System.Collections.Concurrent.ConcurrentDictionary<int, AttackCooldown>();

        public bool inGlobalCooldown = false;
        public float globalCooldownTimeLeft = 0f;

        #endregion

        #region "Events"

        public event CharacterDiedEventHandler OnDeath;

        public event TakeDamageEventHandler OnTakeDamage;

        #endregion

        private CombatEntity() { }

        internal CombatEntity(Classes.Character _character) {
            character = _character;
            combatStatModifiers = new List<CombatStatModifier>();

            CalculateBaseStats();
            CalculateCurrentStats();

            var combatSystem = SystemLocator.GetService<ICombatSystem>();

            maxHealth = combatSystem.CalculateHealth(currentStats);
            currentHealth = maxHealth;
        }

        private void CalculateBaseStats() {
            var stats = character.Stats;

            baseStats = stats;
        }

        private void CalculateCurrentStats() {
            var stats = baseStats;

            foreach (var modifier in combatStatModifiers) {
                stats += modifier.statModifier.Stats;
            }

            currentStats = stats;
        }

        #region Attacking

        public AttackResult PerformAttack(CombatEntity entityToAttack, Classes.Attack attackToPerform) {
            var res = new AttackResult();

            if (cooldowns.ContainsKey(attackToPerform.a_ID)) {
                res.success = false;
                res.reason = AttackResultFailReason.InCooldown;
            } else if (inGlobalCooldown) {
                res.success = false;
                res.reason = AttackResultFailReason.InGlobalCooldown;
            } else {
                //can attack
                if (attackToPerform.a_PreparationDuration == 0) {
                    if (attackToPerform.a_AttackType == Enums.AttackType.Instant) {
                        //perform the instant attack, dealing damage, putting that attack on cooldown if needed, igniting the global cooldown
                        var combatSystem = SystemLocator.GetService<ICombatSystem>();
                        var damage = combatSystem.CalculateDamage(this, entityToAttack, attackToPerform);

                        var takeDamageResult = entityToAttack.TakeDamage(damage, this);
                        
                        if (attackToPerform.a_Cooldown > 0) {
                            var ac = AttackCooldown.GetAttackCooldown(attackToPerform);
                            if (ac != null) {
                                cooldowns[attackToPerform.a_ID] = ac;
                            }
                        }

                        inGlobalCooldown = true;
                        globalCooldownTimeLeft = GLOBAL_ATTACK_COOLDOWN;

                        res.success = true;
                        res.action = AttackResultSuccessAction.AttackFinished;
                        res.takeDamageResult = takeDamageResult;
                    } else {
                        //TODO: tell the interface to spawn a projectile, pass back the projectile data to spawn.
                        /**
                         * 
                         * I suppose these projectiles need:
                         *  - speed
                         *  - renderer/mesh
                         * 
                         */
                        throw new NotImplementedException();
                    }
                } else {
                    //TODO: start casting, return a timer object
                    throw new NotImplementedException();
                }
            }

            return res;
        }

        public TakeDamageResult TakeDamage(long damage, CombatEntity entity = null) {
            var combatSystem = SystemLocator.GetService<ICombatSystem>();

            var res = new TakeDamageResult() {
                damage = damage,
                damageSource = entity
            };

            currentHealth -= damage;

            if (currentHealth <= 0) { //combat entity died
                res.died = true;
                combatState = CombatState.Dead;

                var deathReason = new DeathReason();
                if (entity == null) {
                    deathReason.source = DeathSource.Environment;
                } else {
                    deathReason.source = DeathSource.Character;
                    deathReason.characterSource = entity.character;
                }

                //Fire this instances death event
                OnDeath?.Invoke(this, new CharacterDiedEventArgs() {
                    Character = character,
                    Reason = deathReason
                });

                //Fire the global character died event
                combatSystem.CharacterDied(character, deathReason);
            }

            OnTakeDamage?.Invoke(this, new TakeDamageEventArgs() {
                Result = res
            });

            return res;
        }

        #endregion

        public void Update(float deltaTime) {
            if (inGlobalCooldown) {
                globalCooldownTimeLeft -= deltaTime;

                if (globalCooldownTimeLeft <= 0f) {
                    inGlobalCooldown = false;
                    globalCooldownTimeLeft = 0f;
                }
            }

            var cooldownsFinished = new Dictionary<int, AttackCooldown>();
            foreach (var a_ID in cooldowns.Keys) {
                var cooldown = cooldowns[a_ID];

                cooldown.timeLeft -= deltaTime;

                if (cooldown.timeLeft <= 0f) {
                    cooldownsFinished.Add(a_ID, cooldown);
                }
            }
            foreach (var a_ID in cooldownsFinished.Keys) {
                try {
                    cooldowns.TryRemove(a_ID, out var cd);
                } catch (Exception) {
                    //TODO: exception handling
                }
            }
        }
    }

    public enum CombatState {
        OutOfCombat = 1,
        Attacking = 2,
        GlobalCooldown = 3,
        Idle = 4,
        Casting = 5,
        Dead = 6
    }


    #region "Attacking enums/classes"

    public class AttackCooldown {
        public Classes.Attack attack;
        public DateTime attackPerformed { get; private set; }
        public float timeLeft;

        AttackCooldown(Attack attack) {
            attackPerformed = GeneralOps.Now;
            timeLeft = attack.a_Cooldown;
        }

        #region "Factory function"

        public static AttackCooldown GetAttackCooldown(Attack attack) {
            if (attack.a_Cooldown > 0) {
                return new AttackCooldown(attack);
            } else {
                return null;
            }
        }

        #endregion
    }

    public class AttackResult {
        public bool success;
        public AttackResultFailReason reason;
        public AttackResultSuccessAction action;

        #region "Start Casting"



        #endregion

        #region "Damage"

        public TakeDamageResult takeDamageResult;

        #endregion
    }

    public enum AttackResultFailReason {
        None = 0,
        InCooldown = 1,
        InGlobalCooldown = 2
    }

    public enum AttackResultSuccessAction {
        AttackFinished = 0,
        StartCasting = 1,
        Projectile = 2
    }

    #endregion

    public class CombatStatModifier {
        public Classes.StatModifier statModifier;
        public DateTime started;
        public DateTime lastTick;
    }

    /// <summary>
    /// A helper class to assist with damage calculations
    /// </summary>
    public class DamageResult {
        public long damage;
    }

    public class TakeDamageResult {
        public bool died = false;
        public long damage = 0;
        public CombatEntity damageSource = null;
    }

    public class DeathReason {
        public DeathSource source;
        public Classes.Character characterSource;
    }

    public enum DeathSource {
        Character = 1,
        Environment = 2
    }

    #region "Event Handling"

    public delegate void CharacterDiedEventHandler(object sender, CharacterDiedEventArgs e);

    public class CharacterDiedEventArgs : EventArgs {
        public Classes.Character Character { get; set; }
        public DeathReason Reason { get; set; }
    }

    public delegate void TakeDamageEventHandler(object sender, TakeDamageEventArgs e);

    public class TakeDamageEventArgs : EventArgs {
        public TakeDamageResult Result;
    }

    #endregion
}
