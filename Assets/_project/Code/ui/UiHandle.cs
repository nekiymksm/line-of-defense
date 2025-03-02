using System.Collections.Generic;
using _project.Code.ui.@base;
using _project.Code.ui.data;
using _project.Code.ui.enums;
using _project.Code.utility.SampleSelection;
using UnityEngine;
using Zenject;

namespace _project.Code.ui
{
    public class UiHandle : MonoBehaviour
    {
        [SerializeField] private UiElementsCollection _uiCollection;
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private Canvas _extraCanvas;

        private DiContainer _container;
        private Transform _targetTransform;
        private List<UiElement> _activeUiElements;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        
        private void Awake()
        {
            _targetTransform = _mainCanvas.transform;
            _activeUiElements = new List<UiElement>();
        }

        public bool CanShow<T>(CanvasKind targetCanvas, out T uiElement) where T : UiElement
        {
            if (SampleSelection.ContainsByType<T,UiElement>(_activeUiElements) == false &&
                _uiCollection.HasElement(out T element))
            {
                switch (targetCanvas)
                {
                    case CanvasKind.Main:
                        _targetTransform = _mainCanvas.transform;
                        break;
                    
                    case CanvasKind.Extra:
                        _targetTransform = _extraCanvas.transform;
                        break;
                }
                
                var uiItem = Instantiate(element, _targetTransform);
                uiItem.Show(_container, this);
                _activeUiElements.Add(uiItem);

                uiElement = uiItem;
                return true;
            }

            uiElement = null;
            return false;
        }

        public void TryHide<T>(T element) where T : UiElement
        {
            if (SampleSelection.ContainsByType<T,UiElement>(_activeUiElements))
            {
                foreach (var item in _activeUiElements)
                {
                    if (item.GetType() == typeof(T))
                    {
                        _activeUiElements.Remove(item);
                    }
                }
            }
            
            Destroy(element.gameObject);
        }
    }
}