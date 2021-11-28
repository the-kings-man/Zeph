using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class SelectionIndicator : MonoBehaviour {
        public Transform currentTarget;

        void LateUpdate() {
            if (currentTarget) {
                this.transform.position = currentTarget.position;
            }
        }
    }

}
