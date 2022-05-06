// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

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
            ""name"": ""PlayerShipControls"",
            ""id"": ""8b5d4619-4e04-4aa9-ae14-a80e66565f96"",
            ""actions"": [
                {
                    ""name"": ""PlayerMovement"",
                    ""type"": ""Value"",
                    ""id"": ""c8b4b72a-3f50-4edb-9c6a-da327436ef4e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""ee2bdde1-94db-48c6-8de2-5e9d4f339ca8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Value"",
                    ""id"": ""b415c2a3-0aba-44b0-8d61-0d341f64b724"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""1e5670c8-0659-48ed-b9e2-66520e78112f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a5450ad1-71ee-4c7e-8855-abb1862af285"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""14db5e9f-488a-4cfd-bc7d-db020dc48320"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""95b3333f-0fac-4706-a4b0-05413e188504"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ca4c946f-0a8a-4eb6-9f8e-246ff00bfff2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""52cf4cae-54d8-4ca1-84f7-f56c5f46e9c2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c35aa96-78f6-4017-8423-309a2f5c1e7a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UIControls"",
            ""id"": ""2a507d5a-4bc4-4de3-83f3-d88afa99c222"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""f78bdb01-5dcb-4870-a837-9a2105c816fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4787f7d4-b561-4f6b-b32e-da1d8a5aee90"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerShipControls
        m_PlayerShipControls = asset.FindActionMap("PlayerShipControls", throwIfNotFound: true);
        m_PlayerShipControls_PlayerMovement = m_PlayerShipControls.FindAction("PlayerMovement", throwIfNotFound: true);
        m_PlayerShipControls_Shoot = m_PlayerShipControls.FindAction("Shoot", throwIfNotFound: true);
        m_PlayerShipControls_Pause = m_PlayerShipControls.FindAction("Pause", throwIfNotFound: true);
        // UIControls
        m_UIControls = asset.FindActionMap("UIControls", throwIfNotFound: true);
        m_UIControls_Pause = m_UIControls.FindAction("Pause", throwIfNotFound: true);
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

    // PlayerShipControls
    private readonly InputActionMap m_PlayerShipControls;
    private IPlayerShipControlsActions m_PlayerShipControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerShipControls_PlayerMovement;
    private readonly InputAction m_PlayerShipControls_Shoot;
    private readonly InputAction m_PlayerShipControls_Pause;
    public struct PlayerShipControlsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerShipControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMovement => m_Wrapper.m_PlayerShipControls_PlayerMovement;
        public InputAction @Shoot => m_Wrapper.m_PlayerShipControls_Shoot;
        public InputAction @Pause => m_Wrapper.m_PlayerShipControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerShipControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerShipControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerShipControlsActions instance)
        {
            if (m_Wrapper.m_PlayerShipControlsActionsCallbackInterface != null)
            {
                @PlayerMovement.started -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnPlayerMovement;
                @PlayerMovement.performed -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnPlayerMovement;
                @PlayerMovement.canceled -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnPlayerMovement;
                @Shoot.started -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnShoot;
                @Pause.started -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerShipControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerShipControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMovement.started += instance.OnPlayerMovement;
                @PlayerMovement.performed += instance.OnPlayerMovement;
                @PlayerMovement.canceled += instance.OnPlayerMovement;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerShipControlsActions @PlayerShipControls => new PlayerShipControlsActions(this);

    // UIControls
    private readonly InputActionMap m_UIControls;
    private IUIControlsActions m_UIControlsActionsCallbackInterface;
    private readonly InputAction m_UIControls_Pause;
    public struct UIControlsActions
    {
        private @PlayerControls m_Wrapper;
        public UIControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_UIControls_Pause;
        public InputActionMap Get() { return m_Wrapper.m_UIControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIControlsActions set) { return set.Get(); }
        public void SetCallbacks(IUIControlsActions instance)
        {
            if (m_Wrapper.m_UIControlsActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIControlsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_UIControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public UIControlsActions @UIControls => new UIControlsActions(this);
    public interface IPlayerShipControlsActions
    {
        void OnPlayerMovement(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IUIControlsActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
}
