//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Scripts/Input/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ClashOfClans
{
    public partial class @Controls: IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""1a888749-a1a5-412e-8994-b9ceafe4b280"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""a98966dd-b758-4928-9191-298685b038b4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveDelta"",
                    ""type"": ""Value"",
                    ""id"": ""84b4c92d-f195-4664-a83c-071233701b2f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""Value"",
                    ""id"": ""f7d66ec1-6a74-4cdb-9e30-983ebaf10033"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""6fd91212-799b-4e61-bd55-2a301a66de51"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchZoom"",
                    ""type"": ""Button"",
                    ""id"": ""73e237d6-45d6-4c27-a4df-8b325190ce91"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchPositon0"",
                    ""type"": ""Value"",
                    ""id"": ""1c35c31c-8b65-4b78-99cc-3394229066ad"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TouchPositon1"",
                    ""type"": ""Value"",
                    ""id"": ""c9780faf-3db9-4af6-84c2-bce38fe13d01"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""19720609-43a6-4218-948d-adee67615bf5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0ba19e9-7cf6-4e61-b94c-e86bc9101076"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84631f20-b5b3-483d-a130-7f5826faf276"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""beefbc51-e19f-4cda-bebe-d995f38f6d1c"",
                    ""path"": ""<Touchscreen>/primaryTouch/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa651752-26be-482b-8851-d9b2e6c9e27a"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f62570eb-ecb9-4cc3-8eed-21fc4ef3defb"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""20a4ac92-e3ff-4f76-8bd4-df4483c3559d"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""a1f1b622-5dc7-4946-a42e-64066062d662"",
                    ""path"": ""<Touchscreen>/touch0/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""57dad093-2809-401f-b516-eb5dcbcce47b"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""60f3c73d-060f-46aa-bb64-6e03d20e9bfe"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPositon0"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48b48455-b070-4a8f-8363-4bbf5ae9a6de"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TouchPositon1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Main
            m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
            m_Main_Move = m_Main.FindAction("Move", throwIfNotFound: true);
            m_Main_MoveDelta = m_Main.FindAction("MoveDelta", throwIfNotFound: true);
            m_Main_MouseScroll = m_Main.FindAction("MouseScroll", throwIfNotFound: true);
            m_Main_MousePosition = m_Main.FindAction("MousePosition", throwIfNotFound: true);
            m_Main_TouchZoom = m_Main.FindAction("TouchZoom", throwIfNotFound: true);
            m_Main_TouchPositon0 = m_Main.FindAction("TouchPositon0", throwIfNotFound: true);
            m_Main_TouchPositon1 = m_Main.FindAction("TouchPositon1", throwIfNotFound: true);
        }

        ~@Controls()
        {
            UnityEngine.Debug.Assert(!m_Main.enabled, "This will cause a leak and performance issues, Controls.Main.Disable() has not been called.");
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

        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }

        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Main
        private readonly InputActionMap m_Main;
        private List<IMainActions> m_MainActionsCallbackInterfaces = new List<IMainActions>();
        private readonly InputAction m_Main_Move;
        private readonly InputAction m_Main_MoveDelta;
        private readonly InputAction m_Main_MouseScroll;
        private readonly InputAction m_Main_MousePosition;
        private readonly InputAction m_Main_TouchZoom;
        private readonly InputAction m_Main_TouchPositon0;
        private readonly InputAction m_Main_TouchPositon1;
        public struct MainActions
        {
            private @Controls m_Wrapper;
            public MainActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Main_Move;
            public InputAction @MoveDelta => m_Wrapper.m_Main_MoveDelta;
            public InputAction @MouseScroll => m_Wrapper.m_Main_MouseScroll;
            public InputAction @MousePosition => m_Wrapper.m_Main_MousePosition;
            public InputAction @TouchZoom => m_Wrapper.m_Main_TouchZoom;
            public InputAction @TouchPositon0 => m_Wrapper.m_Main_TouchPositon0;
            public InputAction @TouchPositon1 => m_Wrapper.m_Main_TouchPositon1;
            public InputActionMap Get() { return m_Wrapper.m_Main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void AddCallbacks(IMainActions instance)
            {
                if (instance == null || m_Wrapper.m_MainActionsCallbackInterfaces.Contains(instance)) return;
                m_Wrapper.m_MainActionsCallbackInterfaces.Add(instance);
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MoveDelta.started += instance.OnMoveDelta;
                @MoveDelta.performed += instance.OnMoveDelta;
                @MoveDelta.canceled += instance.OnMoveDelta;
                @MouseScroll.started += instance.OnMouseScroll;
                @MouseScroll.performed += instance.OnMouseScroll;
                @MouseScroll.canceled += instance.OnMouseScroll;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @TouchZoom.started += instance.OnTouchZoom;
                @TouchZoom.performed += instance.OnTouchZoom;
                @TouchZoom.canceled += instance.OnTouchZoom;
                @TouchPositon0.started += instance.OnTouchPositon0;
                @TouchPositon0.performed += instance.OnTouchPositon0;
                @TouchPositon0.canceled += instance.OnTouchPositon0;
                @TouchPositon1.started += instance.OnTouchPositon1;
                @TouchPositon1.performed += instance.OnTouchPositon1;
                @TouchPositon1.canceled += instance.OnTouchPositon1;
            }

            private void UnregisterCallbacks(IMainActions instance)
            {
                @Move.started -= instance.OnMove;
                @Move.performed -= instance.OnMove;
                @Move.canceled -= instance.OnMove;
                @MoveDelta.started -= instance.OnMoveDelta;
                @MoveDelta.performed -= instance.OnMoveDelta;
                @MoveDelta.canceled -= instance.OnMoveDelta;
                @MouseScroll.started -= instance.OnMouseScroll;
                @MouseScroll.performed -= instance.OnMouseScroll;
                @MouseScroll.canceled -= instance.OnMouseScroll;
                @MousePosition.started -= instance.OnMousePosition;
                @MousePosition.performed -= instance.OnMousePosition;
                @MousePosition.canceled -= instance.OnMousePosition;
                @TouchZoom.started -= instance.OnTouchZoom;
                @TouchZoom.performed -= instance.OnTouchZoom;
                @TouchZoom.canceled -= instance.OnTouchZoom;
                @TouchPositon0.started -= instance.OnTouchPositon0;
                @TouchPositon0.performed -= instance.OnTouchPositon0;
                @TouchPositon0.canceled -= instance.OnTouchPositon0;
                @TouchPositon1.started -= instance.OnTouchPositon1;
                @TouchPositon1.performed -= instance.OnTouchPositon1;
                @TouchPositon1.canceled -= instance.OnTouchPositon1;
            }

            public void RemoveCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterfaces.Remove(instance))
                    UnregisterCallbacks(instance);
            }

            public void SetCallbacks(IMainActions instance)
            {
                foreach (var item in m_Wrapper.m_MainActionsCallbackInterfaces)
                    UnregisterCallbacks(item);
                m_Wrapper.m_MainActionsCallbackInterfaces.Clear();
                AddCallbacks(instance);
            }
        }
        public MainActions @Main => new MainActions(this);
        public interface IMainActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnMoveDelta(InputAction.CallbackContext context);
            void OnMouseScroll(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
            void OnTouchZoom(InputAction.CallbackContext context);
            void OnTouchPositon0(InputAction.CallbackContext context);
            void OnTouchPositon1(InputAction.CallbackContext context);
        }
    }
}
