using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _project.Scripts.ModuleInput
{
    public class AimingInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        private bool _isDown;
        private bool _isPointerOnRightSide;
        private int _screenHalfValue;
        private Vector2 _currentPointerPosition;

        public float PointerHorizontalValue { get; private set; }

        public event Action DownPointer;
        public event Action UpPointer;

        private void Start()
        {
            _isDown = false;
            _isPointerOnRightSide = true;
            _screenHalfValue = Screen.width / 2;
            _currentPointerPosition = new Vector2();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDown = true;
            SetPointerValue();
            
            DownPointer?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDown = false;
            PointerHorizontalValue = 0;
            
            UpPointer?.Invoke();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (_isDown)
            {
                _currentPointerPosition = eventData.position;
                SetPointerValue();
            }
        }

        private void SetPointerValue()
        {
            _isPointerOnRightSide = _currentPointerPosition.x > 0 && _currentPointerPosition.x <= _screenHalfValue;
            
            PointerHorizontalValue = _isPointerOnRightSide
                ? _currentPointerPosition.x / _screenHalfValue - 1
                : (_currentPointerPosition.x - _screenHalfValue) / _screenHalfValue;
        }
    }
}