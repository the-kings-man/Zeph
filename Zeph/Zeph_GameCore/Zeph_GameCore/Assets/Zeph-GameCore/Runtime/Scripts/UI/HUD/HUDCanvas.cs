using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZephGame {
    public class HUDCanvas : MonoBehaviour {

        public static HUDCanvas Instance {
            get { return s_Instance; }
        }

        protected static HUDCanvas s_Instance;

        public GameObject playerQuestHUDCanvas;

        protected Canvas m_Canvas;

        void Awake() {
            if (s_Instance == null)
                s_Instance = this;
            else if (s_Instance != this)
                throw new UnityException("There cannot be more than one HUDCanvas script.  The instances are " + s_Instance.name + " and " + name + ".");

            m_Canvas = this.gameObject.GetComponent<Canvas>();
        }

        void Start() {
        }

    }
}
