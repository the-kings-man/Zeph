using System;
using System.Collections.Generic;
using System.Text;

namespace Zeph.Core.Combat {
    public class CombatSystem {
        public DamageResult CalculateDamage(CombatEntity from, CombatEntity to, Classes.Attack attack) {
            var statsFrom = from.character.Stats;
            var statsTo = to.character.Stats;

            var res = new DamageResult();

            //Melee strike
            switch (attack.a_AttackType) {
                case Enums.AttackType.Strike:
                    res.damage = statsFrom.s_Strength * attack.a_Damage - statsTo.s_Hardness; //obvs oversimplified... Can definitely foresee this requiring some kind of script linked to the attack so that the game designer can fine-tune the combat system.
                    break;
                case Enums.AttackType.Projectile:
                    throw new NotImplementedException();
                    break;
                case Enums.AttackType.Spell:
                    throw new NotImplementedException();
                    break;
            }
            

            return res;
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

    public class CombatEntity {
        public Classes.Character character;
    }

    public class DamageResult {
        public long damage;
    }
}
