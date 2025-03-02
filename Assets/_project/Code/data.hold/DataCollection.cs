using _project.Code.utility.SampleSelection;
using UnityEngine;

namespace _project.Code.data.hold
{
    [CreateAssetMenu(fileName = "DataCollection", menuName = "Data/DataCollection")]
    public class DataCollection : ScriptableObject
    {
        [SerializeField] private ScriptableObject[] _dataCollection;

        public T GetData<T>() where T : ScriptableObject
        {
            if (SampleSelection.HasSampleByType(_dataCollection, out T data))
            {
                return data;
            }

            return null;
        }
    }
}