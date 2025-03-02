using UnityEngine;
using Zenject;

namespace _project.Code.ui.@base
{
    public abstract class UiElement : MonoBehaviour
    {
        private bool _isActive;
        
        protected DiContainer Container;
        protected UiHandle UiHandle;

        private void Awake()
        {
            _isActive = false;
        }

        public void Show(DiContainer container, UiHandle uiHandle)
        {
            if (_isActive)
            {
                return;
            }

            _isActive = true;
            Container = container;
            UiHandle = uiHandle;
            
            OnShow();
        }

        public void Hide()
        {
            OnHide();
            UiHandle.TryHide(this);
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }
    }
}