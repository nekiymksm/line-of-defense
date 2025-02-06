using UnityEngine;

namespace _project.Code.enemy.data
{
    [CreateAssetMenu(fileName = "EnemiesSpawnConfig", menuName = "Data/Enemy/EnemiesSpawnConfig")]
    public class EnemiesSpawnConfig : ScriptableObject
    {
        [SerializeField][Range(0, 10)] private float minSpawnDelay;
        [SerializeField][Range(0, 10)] private float maxSpawnDelay;

        public float GetSpawnDelay()
        {
            return Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }
}