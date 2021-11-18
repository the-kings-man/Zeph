using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeph.Unity {
    public static class GeneralOps {


        public static void Initialise() {
            Login();
        }

        #region Dialog
        public static void StartDialog(Zeph.Core.Classes.Dialog dialog) {
            DialogCanvas.Instance.StartDialog(dialog);
        }
        #endregion

        #region Quest
        public static void StartQuest(Zeph.Core.Classes.Quest quest) {
            QuestCanvas.Instance.ShowQuest(quest, true);
        }
        public static void HandInQuest(Zeph.Core.Classes.Quest quest) {
            QuestCanvas.Instance.ShowHandInQuest(quest);
        }

        public static void ShowQuest(Zeph.Core.Classes.Quest quest) {
            QuestCanvas.Instance.ShowQuest(quest, false);
        }
        #endregion


        #region Player

        static Zeph.Core.Classes.Player _player;

        public static Zeph.Core.Classes.Player CurrentPlayer {
            get {
                return _player;
            }
        }

        static Zeph.Core.Combat.CombatEntity _playerCombatEntity;

        public static Zeph.Core.Combat.CombatEntity PlayerCombatEntity {
            get {
                return _playerCombatEntity;
            }
        }

        public static void Login() {
            _player = Zeph.Core.Classes.Player.Read(1);
            var combatSystem = Zeph.Core.SystemLocator.GetService<Zeph.Core.Combat.ICombatSystem>();
            _playerCombatEntity = combatSystem.GenerateCombatEntity(_player.Character);
        }

        #endregion

    }
}
