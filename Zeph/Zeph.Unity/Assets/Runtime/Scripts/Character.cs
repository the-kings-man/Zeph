using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Character : Entity {

        public override EntityType type { get; protected set; } = EntityType.Character;

        public bool isInteracting = false;

        public Zeph.Core.Classes.Character character { get; private set; }
        public int characterID = -1;

        public CharacterCombat characterCombat { get; private set; }

        // Start is called before the first frame update
        virtual protected void Awake() {
            if (characterID == -1) {
                throw new Zeph.Core.Classes.ExceptionHandling.GeneralException("Zeph.Unity.Character", 1, "Must specify a characterID.");
            } else {
                character = Zeph.Core.Classes.Character.Read(characterID);
            }

            characterCombat = this.gameObject.AddComponent<CharacterCombat>();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}
