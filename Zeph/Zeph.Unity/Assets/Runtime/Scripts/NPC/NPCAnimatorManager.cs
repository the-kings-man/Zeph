using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zeph.Unity {
    public class NPCAnimatorManager : AnimatorManager {
        NPCLocomotion npcLocomotion;

        protected override void Awake() {
            base.Awake();

            npcLocomotion = GetComponent<NPCLocomotion>();
        }

        private void OnAnimatorMove() {
            float delta = Time.deltaTime;

            npcLocomotion.characterRigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            npcLocomotion.characterRigidbody.velocity = velocity * npcLocomotion.walkingSpeed;
        }
    }
}
