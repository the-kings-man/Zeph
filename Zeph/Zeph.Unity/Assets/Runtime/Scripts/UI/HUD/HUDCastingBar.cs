using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zeph.Unity {
    public class HUDCastingBar : MonoBehaviour {
        HUDStatBar progressBar;

        // Start is called before the first frame update
        void Awake()
        {
            progressBar = GetComponentInChildren<HUDStatBar>();
        }

        public void HandleRefresh(long progress, long target) {
            if (progressBar != null) progressBar.HandleRefresh(true, progress, target);
        }
    }
}