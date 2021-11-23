using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace ZephGame {
    public class InteractOnButton : InteractOnTrigger {

        public string buttonName = "Interact";
        public UnityEvent OnButtonPress;

        bool canExecuteButtons = false;

        protected override void ExecuteOnEnter(Collider other) {
            //if (other.GetComponent<PlayerController>() != null) {
                canExecuteButtons = true;

                if (Debug.isDebugBuild) {
                    Debug.Log(other.ToString() + " entered");
                }
            //}
        }

        protected override void ExecuteOnExit(Collider other) {
            //if (other.GetComponent<PlayerController>() != null) {
                canExecuteButtons = false;

                if (Debug.isDebugBuild) {
                    Debug.Log(other.ToString() + " exited");
                }
            //}

        }

        void Update() {
            //if (canExecuteButtons && Input.GetButtonDown(buttonName)) {
                OnButtonPress.Invoke();

                if (Debug.isDebugBuild) {
                    Debug.Log(buttonName + " pressed");
                }
            //}
        }

    }
}
