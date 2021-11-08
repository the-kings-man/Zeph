//using System;
//using System.Collections.Generic;
//using System.Text;

namespace Zeph.Core.Combat {
    public class CombatSystem {
        //        public DamageResult CalculateDamage(Classes.Character characterFrom, Classes.Character characterTo, Classes.Attack attack) {
        //            var statsFrom = characterFrom.Stats;
        //            var statsTo = characterTo.Stats;
        //        }

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
}

//    public class DamageResult {
//        public long damage;
//    }
//}
