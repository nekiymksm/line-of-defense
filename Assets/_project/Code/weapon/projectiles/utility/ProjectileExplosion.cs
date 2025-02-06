using _project.Code.utility;
using UnityEngine;

namespace _project.Code.weapon.projectiles.utility
{
    public class ProjectileExplosion : MonoBehaviour
    {
        private int _damageValue;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IHittable enemy))
            {
                enemy.TakeHit(_damageValue);
            }
        }
        
        public void Initialize(float lifeTime, int damageValue)
        {
            _damageValue = damageValue;
            var itemObject = gameObject;
            
            transform.SetParent(null);
            itemObject.SetActive(true);
            Destroy(itemObject, lifeTime);
        }
    }
}