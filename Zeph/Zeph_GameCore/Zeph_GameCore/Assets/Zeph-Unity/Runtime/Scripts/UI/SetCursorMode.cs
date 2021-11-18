using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zeph.Unity
{
    public class SetCursorMode : MonoBehaviour
    {
        public bool visible = true;
        public CursorLockMode lockMode = CursorLockMode.None;

        void Start()
        {
            Cursor.visible = visible;
            Cursor.lockState = lockMode;
        }
    }
}