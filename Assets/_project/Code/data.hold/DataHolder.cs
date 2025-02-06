using _project.Code.utility;
using UnityEngine;

namespace _project.Code.data.hold
{
    public class DataHolder : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] dataCollection;

        public T GetData<T>() where T : ScriptableObject
        {
            return SampleSelection.Select<T, ScriptableObject>(dataCollection);
        }
    }
}