using Jint.Runtime.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using Zeph.Core.Classes;

namespace Zeph.Core.Combat {
    public interface ICombatSystem {
        DamageResult CalculateDamage(CombatEntity from, CombatEntity to, Classes.Attack attack);
        long CalculateHealth(Stats stats);
        CombatEntity GenerateCombatEntity(Classes.Character character);
        void NPCDied(Classes.NPC npc, DeathReason reason);
        event CharacterDiedEventHandler OnNPCDeath;
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

        public event CharacterDiedEventHandler OnNPCDeath;

        #region "Scripts"

        string calculateDamageScript = @"
if (attack.a_AttackType == AttackType.Strike) {
    return from.currentStats.s_Strength * attack.a_Damage - to.currentStats.s_Hardness;
} else if (attack.a_AttackType == AttackType.Projectile) {
    return 0;
} else if (attack.a_AttackType == AttackType.Spell) {
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

        public DamageResult CalculateDamage(CombatEntity from, CombatEntity to, Classes.Attack attack) {
            var statsFrom = from.character.Stats;
            var statsTo = to.character.Stats;

            var res = new DamageResult();

            var engine = new Jint.Engine();

            engine.SetValue("AttackType", TypeReference.CreateTypeReference(engine, typeof(Enums.AttackType)))
                .SetValue("from", from)
                .SetValue("to", to)
                .SetValue("attack", attack);

            res.damage = Convert.ToInt64(engine.Evaluate(calculateDamageScript).ToObject());
            

            return res;
        }

        public long CalculateHealth(Stats stats) {
            var engine = new Jint.Engine();

            engine.SetValue("stats", stats);

            return Convert.ToInt64(engine.Evaluate(calculateHealthScript).ToObject());
        }

        public void NPCDied(NPC npc, DeathReason reason) {
            OnNPCDeath?.Invoke(null, new CharacterDiedEventArgs() {
                NPC = npc,
                Reason = reason
            });
        }

        #endregion

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

        public Dictionary<int, AttackCooldown> cooldowns;

        public bool inGlobalCooldown = false;
        public DateTime globalCooldownStarted = new DateTime(1900, 1, 1);

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
                        //TODO: perform the instant attack, dealing damage, putting that attack on cooldown if needed, igniting the global cooldown
                        //TODO: The dealing damage function should probably do the death stuff and all that. Death to be just an event which can be handled by anything.
                        //TODO: have a global combat system death event, and possibly a combatEntity instance-specific event
                        throw new NotImplementedException();
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

        #endregion
    }

    public enum CombatState {
        OutOfCombat = 1,
        Attacking = 2,
        GlobalCooldown = 3,
        Idle = 4,
        Casting = 5
    }

    public class AttackCooldown {
        public Classes.Attack attack;
        public DateTime attackPerformed;

        public DateTime CooldownDueToFinish {
            get {
                return attackPerformed.AddMilliseconds(attack.a_Cooldown);
            }
        }
    }

    public class AttackResult {
        public bool success;
        public AttackResultFailReason reason;
        public AttackResultSuccessAction action;

        #region "Start Casting"



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

    public class DeathReason {
        public DeathSource source;
        public Classes.Player player;
        public Classes.NPC npc;
    }

    public enum DeathSource {
        Player = 1,
        NPC = 2,
        Environment = 3
    }

    #region "Event Handling"

    public delegate void CharacterDiedEventHandler(object sender, CharacterDiedEventArgs e);

    public class CharacterDiedEventArgs : EventArgs {
        public Classes.NPC NPC { get; set; }
        public DeathReason Reason { get; set; }
    }

    #endregion
}
