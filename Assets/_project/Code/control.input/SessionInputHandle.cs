using System.Collections.Generic;
using _project.Code.control.input.@base;
using _project.Code.control.input.data;
using _project.Code.control.input.enums;
using _project.Code.data.hold;
using _project.Code.session.state.@base;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _project.Code.control.input
{
    public class SessionInputHandle : MonoBehaviour, IStateObserver
    {
        private InputHandleConfig _inputHandleConfig;
        
        private InputAction _lookAction;
        private InputAction _aimAction;
        
        private Vector2 _lookInputValue;
        private bool _canInput;
        private List<IInputDependable> _inputDependables;
        
        [Inject]
        private void Construct(DataCollection dataCollection)
        {
            _inputHandleConfig = dataCollection.GetData<InputHandleConfig>();
            
            _canInput = true;
            _inputDependables = new List<IInputDependable>();
            
            _lookAction = InputSystem.actions.FindAction(InputActionKind.TurretLook.ToString());
            _aimAction = InputSystem.actions.FindAction(InputActionKind.TurretShot.ToString());
        }

        private void Update()
        {
            _lookInputValue = _lookAction.ReadValue<Vector2>();
            
            if (_canInput == false || _lookInputValue.y < _inputHandleConfig.MinInputZone)
            {
                return;
            }
            
            if (_aimAction.WasPressedThisFrame())
            {
                foreach (var item in _inputDependables)
                {
                    item.PullTrigger();
                }
            }
            
            if (_aimAction.IsPressed())
            {
                foreach (var item in _inputDependables)
                {
                    item.HoldTrigger(_lookInputValue);
                }
            }

            if (_aimAction.WasReleasedThisFrame())
            {
                foreach (var item in _inputDependables)
                {
                    item.ReleaseTrigger();
                }
            }
        }

        public void OnStart()
        {
        }

        public void OnPause()
        {
        }

        public void OnEnd()
        {
            _canInput = false;
            _inputDependables.Clear();
        }

        public void AddInputDependable(IInputDependable item)
        {
            _inputDependables.Add(item);
        }
    }
}