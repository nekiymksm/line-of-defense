using _project.Code.ui.@base;
using _project.Code.utility.SampleSelection;
using UnityEngine;

namespace _project.Code.ui.data
{
    [CreateAssetMenu(fileName = "UiElementsCollection", menuName = "Data/Ui/UiElementsCollection")]
    public class UiElementsCollection : ScriptableObject
    {
        [SerializeField] private UiElement[] _uiElements;

        public bool HasElement<T>(out T element) where T : UiElement
        {
            return SampleSelection.HasSampleByType(_uiElements, out element);
        }
    }
}