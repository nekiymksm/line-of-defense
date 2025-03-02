using UnityEngine;

namespace _project.Code.control.input.data
{
    [CreateAssetMenu(fileName = "InputHandleConfig", menuName = "Data/Input/InputHandleConfig")]
    public class InputHandleConfig : ScriptableObject
    {
        [field: SerializeField, Range(0.0f, 500.0f)] public float MinInputZone { get; private set; }
    }
}