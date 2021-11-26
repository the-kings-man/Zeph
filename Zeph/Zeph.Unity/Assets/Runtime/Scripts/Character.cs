using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Character : Entity {

        public override EntityType type { get; protected set; } = EntityType.Character;

        public bool isInteracting = false;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
