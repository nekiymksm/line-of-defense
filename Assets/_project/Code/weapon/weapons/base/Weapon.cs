using System.Collections;
using _project.Code.weapon.data.weapon.configs.Base;
using _project.Code.weapon.projectiles.Base;
using UnityEngine;

namespace _project.Code.weapon.weapons.Base
{
    public abstract class Weapon
    {
        private Transform _contextTransform;
        private WeaponConfig _weaponConfig;
        private int _projectilesCounter;
        private bool _isLoaded;

        protected WeaponContext WeaponContext;
        protected bool IsPulled;
        protected bool IsReload;

        protected Weapon(WeaponContext weaponContext, WeaponConfig weaponConfig)
        {
            _contextTransform = weaponContext.transform;
            _weaponConfig = weaponConfig;
            _projectilesCounter = 0;
            _isLoaded = true;

            WeaponContext = weaponContext;
            IsPulled = false;
            IsReload = false;
        }

        public void PullTrigger()
        {
            IsPulled = true;
            OnTriggerPull();
        }

        public void HoldTrigger()
        {
            OnTriggerHold();
        }
        
        public void ReleaseTrigger()
        {
            IsPulled = false;
            
            if (_isLoaded)
            {
                IsReload = false;
            }

            OnTriggerRelease();
        }

        protected virtual void OnTriggerPull()
        {
        }
        
        protected virtual void OnTriggerHold()
        {
        }

        protected virtual void OnTriggerRelease()
        {
        }
        
        protected void Shot()
        {
            if (IsReload)
            {
                return;
            }
            
            var projectile = Object.Instantiate(
                _weaponConfig.ProjectilePrefab, 
                _contextTransform.position, 
                _contextTransform.rotation);
            projectile.Initialize(_weaponConfig);

            _projectilesCounter++;
            
            OnShot(projectile);

            if (_projectilesCounter >= _weaponConfig.MagazineCapacity)
            {
                WeaponContext.StartCoroutine(Reload());
            }
        }

        protected virtual void OnShot(Projectile projectile)
        {
        }

        private IEnumerator Reload()
        {
            _isLoaded = false;
            IsReload = true;
            
            yield return new WaitForSeconds(_weaponConfig.ReloadTime);
            
            _projectilesCounter = 0;
            _isLoaded = true;
            IsReload = IsPulled;
        }
    }
}