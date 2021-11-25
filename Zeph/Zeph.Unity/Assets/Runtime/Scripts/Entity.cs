﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Entity : MonoBehaviour {

        public bool isSelectable = true;
        public Entity currentTarget;
        public EntityType type;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

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
