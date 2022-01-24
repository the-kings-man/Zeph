using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeph.Unity {
    public class NPCCombat : CharacterCombat {

        public void HandleAttacking(float distanceFromTarget) {
            if (character.currentTarget) {
                Zeph.Core.Classes.Attack currentAttack = Zeph.Core.Classes.Attack.Read(1);
                if (distanceFromTarget < currentAttack.a_Distance) {
                    base.Attack(character.currentTarget.GetComponent<CharacterCombat>(), currentAttack);
                }
            }
        }
    }
}
