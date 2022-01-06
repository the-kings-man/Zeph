using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class Projectile : MonoBehaviour {

        public Character sourceCharacter;
        public Character targetCharacter;
        public Zeph.Core.Classes.Attack attack;
        public float maxSpeed = 1f;
        public float collisionDistance = 1f;

        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void FixedUpdate() {
            float step = maxSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetCharacter.transform.position, step);

            if (Vector3.Distance(transform.position, targetCharacter.transform.position) < collisionDistance) {
                //TODO: collide, raise an event so the combat system can deal damage and so that other systems can do what they need to also
                targetCharacter.characterCombat.combatEntity.AttackHit(sourceCharacter.characterCombat.combatEntity, attack);
                Destroy(this.gameObject);
            }
        }
    }
}
