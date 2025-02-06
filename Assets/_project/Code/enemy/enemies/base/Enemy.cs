using System;
using _project.Code.enemy.data;
using _project.Code.utility;
using UnityEngine;

namespace _project.Code.enemy.enemies.@base
{
    public abstract class Enemy : MonoBehaviour, IHittable
    {
        private EnemyConfig _config;
        private EnemyMovement _movement;
        private Action<int> _onDefeatAction;
        private Action _onDie;
        private int _health;

        private void Update()
        {
            _movement?.Move();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IHittable hittableItem))
            {
                if (hittableItem.GetType() == GetType())
                {
                    return;
                }
                
                hittableItem.TakeHit(_config.Damage);
                Die();
            }
        }

        public void Initialize(EnemyConfig enemyConfig, Vector3 targetPosition, Action<int> onDefeat, Action onDie)
        {
            _config = enemyConfig;
            _movement = new EnemyMovement(this, targetPosition, enemyConfig.Speed);
            _onDefeatAction = onDefeat;
            _onDie = onDie;
            _health = enemyConfig.Health;
        }

        public void TakeHit(int hitPoints)
        {
            _health -= hitPoints;
            
            if (_health <= 0)
            {
                _onDefeatAction(_config.Points);
                Die();
            }
        }

        public void Die(bool ignoreOnDie = false)
        {
            if (ignoreOnDie == false)
            {
                _onDie();
            }
            
            Destroy(gameObject);
        }
    }
}