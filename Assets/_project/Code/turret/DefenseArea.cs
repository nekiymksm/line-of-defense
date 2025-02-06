using System;
using _project.Code.utility;
using UnityEngine;

namespace _project.Code.turret
{
    public class DefenseArea : MonoBehaviour, IHittable
    {
        private int _health;
        private Action _onHealthEnd;
        
        public void TakeHit(int hitPoints)
        {
            _health -= hitPoints;

            if (_health <= 0)
            {
                _onHealthEnd?.Invoke();
            }
        }

        public void Initialize(int health, Action onHealthEnd)
        {
            _health = health;
            _onHealthEnd = onHealthEnd;
        }
    }
}