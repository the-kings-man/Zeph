using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Entity : MonoBehaviour {

        [Header("Entity Fields")]
        public bool isSelectable = true;
        public Entity currentTarget = null;
        public virtual EntityType type { get; protected set; }

        virtual public void EntitySelected(Entity entity) {
            currentTarget = entity;
        }
    }

    public enum EntityType {
        Character = 1,
        Object = 2,
        Item = 3,
        HarvestableMaterial = 4,
        Loot = 5
    }
}
