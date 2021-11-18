using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZephGame {	
	public class PlayerInput : MonoBehaviour
	{
		[HideInInspector]
		public bool playerControllerInputBlocked;
	
		protected Vector2 m_Movement;
		protected Vector2 m_Camera;
		protected bool m_Jump;
		protected bool m_Pause;
		protected bool m_ExternalInputBlocked;

		public Vector2 MoveInput
		{
			get
			{
				if(playerControllerInputBlocked || m_ExternalInputBlocked)
					return Vector2.zero;
				return m_Movement;
			}
		}

		public Vector2 CameraInput
		{
			get
			{
				if(playerControllerInputBlocked || m_ExternalInputBlocked)
					return Vector2.zero;
				return m_Camera;
			}
		}

		public bool JumpInput
		{
			get { return m_Jump && !playerControllerInputBlocked && !m_ExternalInputBlocked; }
		}
			
		void Awake()
		{
			
		}

		void Update()
		{
			m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
			m_Jump = Input.GetButton("Jump");
			
			m_Pause = Input.GetButtonDown("Cancel");
		}
	}
}