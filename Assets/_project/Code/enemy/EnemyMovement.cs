using _project.Code.enemy.enemies.@base;
using UnityEngine;

namespace _project.Code.enemy
{
    public class EnemyMovement
    {
        private Enemy _currentEnemy;
        private Vector3 _targetPosition;
        private float _speed;

        public EnemyMovement(Enemy enemy, Vector3 position, float speed)
        {
            _currentEnemy = enemy;
            _targetPosition = position;
            _speed = speed;
        }

        public void Move()
        {
            _currentEnemy.transform.position = Vector3.MoveTowards(
                _currentEnemy.transform.position, 
                _targetPosition, 
                Time.deltaTime * _speed);
        }
    }
}