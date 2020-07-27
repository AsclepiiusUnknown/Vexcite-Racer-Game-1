// GENERATED AUTOMATICALLY FROM 'Assets/Input Actions/Input Master.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Master"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""df22ad8e-dc7a-4605-94aa-d8931e567096"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Button"",
                    ""id"": ""90558057-e06e-452a-8386-161d7043c53e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Steering"",
                    ""type"": ""Value"",
                    ""id"": ""b083230d-4cd0-4152-a716-f33a8b7dea79"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Handbrake"",
                    ""type"": ""Button"",
                    ""id"": ""ba984977-6fc0-4831-9f4a-f6a9cdd4ec32"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reverse"",
                    ""type"": ""Button"",
                    ""id"": ""bef52504-d5f8-44e7-af6a-3fdcb81cfd37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turbo"",
                    ""type"": ""Button"",
                    ""id"": ""b093fb96-3cd3-4446-adbb-ca13616efeca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate Car"",
                    ""type"": ""Value"",
                    ""id"": ""41d4d844-9e4b-464f-82bc-e95c4596db92"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look Behind"",
                    ""type"": ""Button"",
                    ""id"": ""2a833908-1cf7-4bf5-80bd-4dfd8595d845"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""49e51424-bb9e-448c-a614-0241c01106f8"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad (General)"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd15b4d9-ca3e-4491-9020-b45943c89fb6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca0d6937-cee9-483e-ac41-576951ff2f49"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad (General)"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""L/R Keys"",
                    ""id"": ""4c93a485-a8b3-440d-88bf-9bd6be3c5a8c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steering"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""64318ca1-0e5a-4340-ac8d-de21dc894001"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4e7573f9-5407-42b8-a169-66f00a9b171e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steering"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bcc84ed0-1531-4cb5-a7ad-c8e34c7f13d9"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad (General)"",
                    ""action"": ""Handbrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36b312fd-1f0c-4d31-8da7-1ca66f577967"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Handbrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""296930e4-8e20-4d8a-a3f4-a712d624fb59"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad (General)"",
                    ""action"": ""Reverse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23aa19c0-fa2a-4db0-b1cf-8b5bab3c3a6e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Reverse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23d9d9dc-4984-400c-962a-395fad4d991d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad (General)"",
                    ""action"": ""Turbo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f1c07e5-84cd-40ca-984e-9458f5cebad4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Turbo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14262edc-f177-4a2c-bcf6-df22269e8dc2"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad (General)"",
                    ""action"": ""Rotate Car"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""ac7a8218-2c81-4dfd-9370-66f976322b6a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate Car"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""82e7eeab-98ae-442e-b307-9a488612638e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate Car"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""04f394c3-0d48-4bc5-9f24-0a0f278550e4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate Car"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d6dce376-3e86-4c19-b196-a5a3350136a4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate Car"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f6613e41-83a5-4c4a-ba72-c5e04af541a8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate Car"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0e65f998-ac68-49ea-88d8-3e714d5af3f0"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad (General)"",
                    ""action"": ""Look Behind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdd186bd-5999-4c49-859c-cd213f3ae27a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Look Behind"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad (General)"",
            ""bindingGroup"": ""Gamepad (General)"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Accelerate = m_Player.FindAction("Accelerate", throwIfNotFound: true);
        m_Player_Steering = m_Player.FindAction("Steering", throwIfNotFound: true);
        m_Player_Handbrake = m_Player.FindAction("Handbrake", throwIfNotFound: true);
        m_Player_Reverse = m_Player.FindAction("Reverse", throwIfNotFound: true);
        m_Player_Turbo = m_Player.FindAction("Turbo", throwIfNotFound: true);
        m_Player_RotateCar = m_Player.FindAction("Rotate Car", throwIfNotFound: true);
        m_Player_LookBehind = m_Player.FindAction("Look Behind", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Accelerate;
    private readonly InputAction m_Player_Steering;
    private readonly InputAction m_Player_Handbrake;
    private readonly InputAction m_Player_Reverse;
    private readonly InputAction m_Player_Turbo;
    private readonly InputAction m_Player_RotateCar;
    private readonly InputAction m_Player_LookBehind;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerate => m_Wrapper.m_Player_Accelerate;
        public InputAction @Steering => m_Wrapper.m_Player_Steering;
        public InputAction @Handbrake => m_Wrapper.m_Player_Handbrake;
        public InputAction @Reverse => m_Wrapper.m_Player_Reverse;
        public InputAction @Turbo => m_Wrapper.m_Player_Turbo;
        public InputAction @RotateCar => m_Wrapper.m_Player_RotateCar;
        public InputAction @LookBehind => m_Wrapper.m_Player_LookBehind;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Accelerate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAccelerate;
                @Steering.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSteering;
                @Steering.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSteering;
                @Steering.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSteering;
                @Handbrake.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHandbrake;
                @Handbrake.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHandbrake;
                @Handbrake.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHandbrake;
                @Reverse.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReverse;
                @Reverse.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReverse;
                @Reverse.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReverse;
                @Turbo.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurbo;
                @Turbo.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurbo;
                @Turbo.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTurbo;
                @RotateCar.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCar;
                @RotateCar.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCar;
                @RotateCar.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotateCar;
                @LookBehind.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookBehind;
                @LookBehind.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookBehind;
                @LookBehind.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLookBehind;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Steering.started += instance.OnSteering;
                @Steering.performed += instance.OnSteering;
                @Steering.canceled += instance.OnSteering;
                @Handbrake.started += instance.OnHandbrake;
                @Handbrake.performed += instance.OnHandbrake;
                @Handbrake.canceled += instance.OnHandbrake;
                @Reverse.started += instance.OnReverse;
                @Reverse.performed += instance.OnReverse;
                @Reverse.canceled += instance.OnReverse;
                @Turbo.started += instance.OnTurbo;
                @Turbo.performed += instance.OnTurbo;
                @Turbo.canceled += instance.OnTurbo;
                @RotateCar.started += instance.OnRotateCar;
                @RotateCar.performed += instance.OnRotateCar;
                @RotateCar.canceled += instance.OnRotateCar;
                @LookBehind.started += instance.OnLookBehind;
                @LookBehind.performed += instance.OnLookBehind;
                @LookBehind.canceled += instance.OnLookBehind;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadGeneralSchemeIndex = -1;
    public InputControlScheme GamepadGeneralScheme
    {
        get
        {
            if (m_GamepadGeneralSchemeIndex == -1) m_GamepadGeneralSchemeIndex = asset.FindControlSchemeIndex("Gamepad (General)");
            return asset.controlSchemes[m_GamepadGeneralSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnAccelerate(InputAction.CallbackContext context);
        void OnSteering(InputAction.CallbackContext context);
        void OnHandbrake(InputAction.CallbackContext context);
        void OnReverse(InputAction.CallbackContext context);
        void OnTurbo(InputAction.CallbackContext context);
        void OnRotateCar(InputAction.CallbackContext context);
        void OnLookBehind(InputAction.CallbackContext context);
    }
}
