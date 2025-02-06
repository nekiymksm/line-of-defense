using _project.Code.utility;
using _project.Code.weapon.data.weapon.configs;
using _project.Code.weapon.data.weapon.configs.Base;
using _project.Code.weapon.projectiles.Base;
using _project.Code.weapon.projectiles.utility;
using UnityEngine;

namespace _project.Code.weapon.projectiles
{
    public class GrenadeLauncherProjectile : Projectile
    {
        [SerializeField] private ProjectileExplosion _projectileExplosion;
        
        [field: SerializeField] public Rigidbody ProjectileRigidbody { get; private set; }

        private GrenadeLauncherConfig _grenadeLauncherConfig;
        
        private void OnDestroy()
        {
            _projectileExplosion
                .Initialize(_grenadeLauncherConfig.ExplosionLifetime, _grenadeLauncherConfig.DamageValue);
        }

        protected override void OnInitialize(WeaponConfig weaponConfig)
        {
            _projectileExplosion.gameObject.SetActive(false);
            _grenadeLauncherConfig = weaponConfig as GrenadeLauncherConfig;
        }

        protected override void OnHit(IHittable enemy)
        {
            Destroy(gameObject);
        }
    }
}