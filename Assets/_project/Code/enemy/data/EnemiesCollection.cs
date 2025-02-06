using System;
using _project.Code.enemy.enemies.@base;
using _project.Code.enemy.enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Code.enemy.data
{
    [CreateAssetMenu(fileName = "EnemiesCollection", menuName = "Data/Enemy/EnemiesCollection")]
    public class EnemiesCollection : ScriptableObject
    {
        [SerializeField] private EnemyConfig[] enemies;

        public EnemyConfig Get()
        {
            return enemies[Random.Range(0, enemies.Length - 1)];
        }
    }

    [Serializable]
    public class EnemyConfig
    {
        public EnemyDifficultyKind EnemyDifficultyKind;
        public Enemy EnemyPrefab;
        public float Speed;
        public int Points;
        public int Health;
        public int Damage;
    }
}