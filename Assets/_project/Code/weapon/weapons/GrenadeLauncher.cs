using System.Collections;
using _project.Code.weapon.data.weapon.configs;
using _project.Code.weapon.projectiles;
using _project.Code.weapon.projectiles.Base;
using _project.Code.weapon.weapons.Base;
using UnityEngine;

namespace _project.Code.weapon.weapons
{
    public class GrenadeLauncher : Weapon
    {
        private GrenadeLauncherConfig _grenadeLauncherConfig;
        private bool _isIntake;
        private float _launchForce;
        
        public GrenadeLauncher(WeaponContext weaponContext, GrenadeLauncherConfig weaponConfig) 
            : base(weaponContext, weaponConfig)
        {
            _grenadeLauncherConfig = weaponConfig;
            _launchForce = weaponConfig.MinLaunchForce;
        }

        protected override void OnTriggerPull()
        {
            if (IsReload)
            {
                return;
            }
            
            WeaponContext.StartCoroutine(IntakeLaunchForce());
        }

        protected override void OnTriggerRelease()
        {
            if (IsReload)
            {
                return;
            }
            
            _isIntake = false; 
            WeaponContext.StopCoroutine(IntakeLaunchForce());
            
            Shot();
        }

        protected override void OnShot(Projectile projectile)
        {
            if (projectile.TryGetComponent(out GrenadeLauncherProjectile grenadeLauncherProjectile))
            {
                grenadeLauncherProjectile.ProjectileRigidbody
                    .AddForce(grenadeLauncherProjectile.transform.forward * _launchForce, ForceMode.Impulse);
                
                _launchForce = _grenadeLauncherConfig.MinLaunchForce;
            }
        }

        private IEnumerator IntakeLaunchForce()
        {
            _isIntake = true;
            
            while (_isIntake)
            {
                yield return new WaitForSeconds(_grenadeLauncherConfig.ForceIntakeRate);
                
                _launchForce = _launchForce >= _grenadeLauncherConfig.MaxLaunchForce
                    ? _grenadeLauncherConfig.MaxLaunchForce
                    : ++_launchForce;
            }
        }
    }
}