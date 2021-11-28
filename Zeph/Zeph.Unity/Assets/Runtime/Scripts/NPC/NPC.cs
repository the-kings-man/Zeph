using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class NPC : Character {
        Animator animator;

        // Start is called before the first frame update
        void Awake() {
            animator = GetComponent<Animator>();
        }

        void Update() {
        }

        void FixedUpdate() {
        }

        void LateUpdate() {
            isInteracting = animator.GetBool("IsInteracting");
            animator.SetBool("IsGrounded", false);
        }
    }
}
