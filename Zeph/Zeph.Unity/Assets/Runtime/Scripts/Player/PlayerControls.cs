// GENERATED AUTOMATICALLY FROM 'Assets/Runtime/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player Movement"",
            ""id"": ""264bf9a7-b9a2-4582-8390-ae718dc5c263"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ff378aa5-0be3-4123-9a8a-271861a692da"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b906554e-b678-46f9-9f9a-d4c99605d9ff"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraFreeLook"",
                    ""type"": ""Button"",
                    ""id"": ""aeeeb8f5-559a-465b-ba96-be1dbea3299d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""c3767b62-5df3-48c9-a11e-1ac73618ce46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""343dcf91-167f-46d6-bc2b-89ad1ffe7c2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMoveInOut"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f4076c14-9ad7-44e2-a050-41a3d4eb9ec8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""90460d69-20c5-4b43-b511-27e65f87d65d"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f7a7a7d3-58fc-4333-a20b-7cb10a35cde2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c1c9c2e3-ec6b-433d-bc43-fe98556f0e44"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2d185ea5-e051-416a-b461-d483d96ba302"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a0c2a5a1-2f35-45f0-b085-43bb73f1b9b0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""2d6d0393-db03-4461-a5bd-8dee0126c83e"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f3c936fb-db4a-40ad-9540-808130ae892d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c5ee0b9f-4e39-416f-867e-66d971bf2bba"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9bc96af0-6228-491c-ba90-4025164f58d0"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""42a8ee0f-2f21-45c4-9a4d-26876bddb68a"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""58caf4fa-6b5f-4c5b-bdbf-4ff1ac75803c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Right Stick"",
                    ""id"": ""c8b905de-7a7f-4ee4-a991-bfe70f5fad5e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""acb0b09d-236a-42b3-ade9-33b4a33468ba"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""70605aa6-834c-49d3-a986-b02616fa2788"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""33fb6597-61d6-4046-87c6-80e4452bb7cf"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""883124f5-c7e2-45ea-bab2-1b0ff9983e68"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6f7b3fcc-96ed-417a-b641-ca62c57ff371"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraFreeLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a19e6639-d57a-49f5-8f75-4e6f31a63829"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e152ea1d-69c1-4a94-bf1e-3ddeee55e912"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a941c74-1dac-4ae9-a271-2af61f967d26"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMoveInOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Actions"",
            ""id"": ""b5bba8d4-e825-4322-957f-3088f151fe57"",
            ""actions"": [
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""9dd44dae-f75c-4efc-b737-a6777672de6f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9487c518-2086-4b9a-95c5-48b2bf7fbc1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""f3100909-2f54-4ff6-ade9-dc1d710560d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""01e045c1-70de-453f-abc3-3de73374a997"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21e8c9eb-b734-47b0-8b22-455fa78a63ed"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fcca0b9-fe06-43eb-a774-0cff7836447a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d83029cc-316f-462d-add8-f927bd2762d6"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40ec6d0f-eb7f-4011-b541-1d5d72631729"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player Hotbars"",
            ""id"": ""1f24e920-a068-4c83-9628-855576d846d9"",
            ""actions"": [
                {
                    ""name"": ""Button1"",
                    ""type"": ""Button"",
                    ""id"": ""18665b18-e787-4f9d-b176-837e14991644"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button2"",
                    ""type"": ""Button"",
                    ""id"": ""c0bdf222-c054-4789-8d47-8c791f81ab0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button3"",
                    ""type"": ""Button"",
                    ""id"": ""21808b9c-7364-48a7-b617-7e2665db84e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button4"",
                    ""type"": ""Button"",
                    ""id"": ""e912de99-f020-4cbf-8b13-1eeb9a799671"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Button5"",
                    ""type"": ""Button"",
                    ""id"": ""80ba9a18-ab10-4171-b8de-74804187e614"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f953e919-6525-484d-bd61-15e5cfb194eb"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""792bd294-1a71-45ae-8287-9014872300a1"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e3938f5-b110-4d06-a5f7-791d01314852"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36fe1213-966d-4a71-b47a-84dd47436fa1"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66f9517a-e9cc-47c1-ba42-79e6f63af518"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Movement
        m_PlayerMovement = asset.FindActionMap("Player Movement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        m_PlayerMovement_CameraFreeLook = m_PlayerMovement.FindAction("CameraFreeLook", throwIfNotFound: true);
        m_PlayerMovement_CameraRotateLeft = m_PlayerMovement.FindAction("CameraRotateLeft", throwIfNotFound: true);
        m_PlayerMovement_CameraRotateRight = m_PlayerMovement.FindAction("CameraRotateRight", throwIfNotFound: true);
        m_PlayerMovement_CameraMoveInOut = m_PlayerMovement.FindAction("CameraMoveInOut", throwIfNotFound: true);
        // Player Actions
        m_PlayerActions = asset.FindActionMap("Player Actions", throwIfNotFound: true);
        m_PlayerActions_Sprint = m_PlayerActions.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerActions_Jump = m_PlayerActions.FindAction("Jump", throwIfNotFound: true);
        m_PlayerActions_Select = m_PlayerActions.FindAction("Select", throwIfNotFound: true);
        // Player Hotbars
        m_PlayerHotbars = asset.FindActionMap("Player Hotbars", throwIfNotFound: true);
        m_PlayerHotbars_Button1 = m_PlayerHotbars.FindAction("Button1", throwIfNotFound: true);
        m_PlayerHotbars_Button2 = m_PlayerHotbars.FindAction("Button2", throwIfNotFound: true);
        m_PlayerHotbars_Button3 = m_PlayerHotbars.FindAction("Button3", throwIfNotFound: true);
        m_PlayerHotbars_Button4 = m_PlayerHotbars.FindAction("Button4", throwIfNotFound: true);
        m_PlayerHotbars_Button5 = m_PlayerHotbars.FindAction("Button5", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player Movement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    private readonly InputAction m_PlayerMovement_CameraFreeLook;
    private readonly InputAction m_PlayerMovement_CameraRotateLeft;
    private readonly InputAction m_PlayerMovement_CameraRotateRight;
    private readonly InputAction m_PlayerMovement_CameraMoveInOut;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputAction @CameraFreeLook => m_Wrapper.m_PlayerMovement_CameraFreeLook;
        public InputAction @CameraRotateLeft => m_Wrapper.m_PlayerMovement_CameraRotateLeft;
        public InputAction @CameraRotateRight => m_Wrapper.m_PlayerMovement_CameraRotateRight;
        public InputAction @CameraMoveInOut => m_Wrapper.m_PlayerMovement_CameraMoveInOut;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @CameraFreeLook.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraFreeLook;
                @CameraFreeLook.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraFreeLook;
                @CameraFreeLook.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraFreeLook;
                @CameraRotateLeft.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraRotateLeft;
                @CameraRotateLeft.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraRotateLeft;
                @CameraRotateLeft.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraRotateLeft;
                @CameraRotateRight.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraRotateRight;
                @CameraRotateRight.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraRotateRight;
                @CameraRotateRight.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraRotateRight;
                @CameraMoveInOut.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraMoveInOut;
                @CameraMoveInOut.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraMoveInOut;
                @CameraMoveInOut.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCameraMoveInOut;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
                @CameraFreeLook.started += instance.OnCameraFreeLook;
                @CameraFreeLook.performed += instance.OnCameraFreeLook;
                @CameraFreeLook.canceled += instance.OnCameraFreeLook;
                @CameraRotateLeft.started += instance.OnCameraRotateLeft;
                @CameraRotateLeft.performed += instance.OnCameraRotateLeft;
                @CameraRotateLeft.canceled += instance.OnCameraRotateLeft;
                @CameraRotateRight.started += instance.OnCameraRotateRight;
                @CameraRotateRight.performed += instance.OnCameraRotateRight;
                @CameraRotateRight.canceled += instance.OnCameraRotateRight;
                @CameraMoveInOut.started += instance.OnCameraMoveInOut;
                @CameraMoveInOut.performed += instance.OnCameraMoveInOut;
                @CameraMoveInOut.canceled += instance.OnCameraMoveInOut;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // Player Actions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Sprint;
    private readonly InputAction m_PlayerActions_Jump;
    private readonly InputAction m_PlayerActions_Select;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Sprint => m_Wrapper.m_PlayerActions_Sprint;
        public InputAction @Jump => m_Wrapper.m_PlayerActions_Jump;
        public InputAction @Select => m_Wrapper.m_PlayerActions_Select;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Sprint.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Select.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSelect;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // Player Hotbars
    private readonly InputActionMap m_PlayerHotbars;
    private IPlayerHotbarsActions m_PlayerHotbarsActionsCallbackInterface;
    private readonly InputAction m_PlayerHotbars_Button1;
    private readonly InputAction m_PlayerHotbars_Button2;
    private readonly InputAction m_PlayerHotbars_Button3;
    private readonly InputAction m_PlayerHotbars_Button4;
    private readonly InputAction m_PlayerHotbars_Button5;
    public struct PlayerHotbarsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerHotbarsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Button1 => m_Wrapper.m_PlayerHotbars_Button1;
        public InputAction @Button2 => m_Wrapper.m_PlayerHotbars_Button2;
        public InputAction @Button3 => m_Wrapper.m_PlayerHotbars_Button3;
        public InputAction @Button4 => m_Wrapper.m_PlayerHotbars_Button4;
        public InputAction @Button5 => m_Wrapper.m_PlayerHotbars_Button5;
        public InputActionMap Get() { return m_Wrapper.m_PlayerHotbars; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerHotbarsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerHotbarsActions instance)
        {
            if (m_Wrapper.m_PlayerHotbarsActionsCallbackInterface != null)
            {
                @Button1.started -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton1;
                @Button1.performed -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton1;
                @Button1.canceled -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton1;
                @Button2.started -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton2;
                @Button2.performed -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton2;
                @Button2.canceled -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton2;
                @Button3.started -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton3;
                @Button3.performed -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton3;
                @Button3.canceled -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton3;
                @Button4.started -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton4;
                @Button4.performed -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton4;
                @Button4.canceled -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton4;
                @Button5.started -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton5;
                @Button5.performed -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton5;
                @Button5.canceled -= m_Wrapper.m_PlayerHotbarsActionsCallbackInterface.OnButton5;
            }
            m_Wrapper.m_PlayerHotbarsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Button1.started += instance.OnButton1;
                @Button1.performed += instance.OnButton1;
                @Button1.canceled += instance.OnButton1;
                @Button2.started += instance.OnButton2;
                @Button2.performed += instance.OnButton2;
                @Button2.canceled += instance.OnButton2;
                @Button3.started += instance.OnButton3;
                @Button3.performed += instance.OnButton3;
                @Button3.canceled += instance.OnButton3;
                @Button4.started += instance.OnButton4;
                @Button4.performed += instance.OnButton4;
                @Button4.canceled += instance.OnButton4;
                @Button5.started += instance.OnButton5;
                @Button5.performed += instance.OnButton5;
                @Button5.canceled += instance.OnButton5;
            }
        }
    }
    public PlayerHotbarsActions @PlayerHotbars => new PlayerHotbarsActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
        void OnCameraFreeLook(InputAction.CallbackContext context);
        void OnCameraRotateLeft(InputAction.CallbackContext context);
        void OnCameraRotateRight(InputAction.CallbackContext context);
        void OnCameraMoveInOut(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
    }
    public interface IPlayerHotbarsActions
    {
        void OnButton1(InputAction.CallbackContext context);
        void OnButton2(InputAction.CallbackContext context);
        void OnButton3(InputAction.CallbackContext context);
        void OnButton4(InputAction.CallbackContext context);
        void OnButton5(InputAction.CallbackContext context);
    }
}
