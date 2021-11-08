using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeph.Test {
    class Program {
        static void Main(string[] args) {
            //Set up the system locator
            Zeph.Core.SystemLocator.CombatSystem = new Zeph.Core.Combat.CombatSystem();
            Zeph.Core.SystemLocator.InventorySystem = new Zeph.Core.Inventory.InventorySystem();


            //Do some damage
            var cs = Zeph.Core.SystemLocator.GetService<Zeph.Core.Combat.ICombatSystem>();

            var player = Zeph.Core.Classes.Player.Read(1);
            var playerCombatEntity = cs.GenerateCombatEntity(player.Character);

            var enemy = Zeph.Core.Classes.NPC.Read(3);
            var enemyCombatEntity = cs.GenerateCombatEntity(enemy.Character);

            var attack = Core.Classes.Attack.Read(1); //melee


            var dmg = cs.CalculateDamage(playerCombatEntity, enemyCombatEntity, attack);

            Console.WriteLine(dmg.damage);

            Console.ReadLine();
        }
    }
}
