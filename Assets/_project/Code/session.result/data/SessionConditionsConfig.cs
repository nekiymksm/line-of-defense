using UnityEngine;

namespace _project.Code.session.result.data
{
    [CreateAssetMenu(fileName = "SessionConditionsConfig", menuName = "Data/Result/SessionConditions")]
    public class SessionConditionsConfig : ScriptableObject
    {
        [field: SerializeField] public int DefenseAreaHealth { get; private set; }
    }
}