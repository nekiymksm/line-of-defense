using _project.Code.utility;
using _project.Code.weapon.data.weapon.configs.Base;
using UnityEngine;

namespace _project.Code.weapon.projectiles.Base
{
    public abstract class Projectile : MonoBehaviour
    {
        protected WeaponConfig WeaponConfig;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IHittable enemy))
            {
                OnHit(enemy);
            }
        }

        public void Initialize(WeaponConfig weaponConfig)
        {
            WeaponConfig = weaponConfig;
            
            Destroy(gameObject, weaponConfig.ProjectileLifetime);
            OnInitialize(weaponConfig);
        }

        protected virtual void OnInitialize(WeaponConfig weaponConfig)
        {
        }
        
        protected virtual void OnHit(IHittable enemy)
        {
        }
    }
}