using System;
using System.Collections;
using _project.Code.control.input.@base;
using _project.Code.tower;
using _project.Code.weapon.data.weapon.configs.Base;
using _project.Code.weapon.projectiles.Base;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _project.Code.weapon.weapons.Base
{
    public abstract class Weapon : IInputDependable
    {
        private WeaponConfig _config;
        private int _index;
        private int _projectilesCounter;
        private bool _isLoaded;
        
        protected WeaponContext WeaponContext;
        protected bool IsPulled;
        protected bool IsReload;
        
        public bool IsActive { private get; set; }

        public event Action<int, float> ReloadStarted;

        protected Weapon(WeaponConfig config, WeaponContext weaponContext, int weaponIndex)
        {
            _index = weaponIndex;
            _config = config;
            WeaponContext = weaponContext;

            _projectilesCounter = 0;
            _isLoaded = true;
            
            IsPulled = false;
            IsReload = false;
            IsActive = false;
        }

        public void PullTrigger()
        {
            if (IsActive == false)
            {
                return;
            }
            
            IsPulled = true;
            WeaponContext.SetPointer(IsPulled);
            
            OnTriggerPull();
        }

        public void HoldTrigger(Vector2 lookValue)
        {
            if (IsActive == false)
            {
                return;
            }
            
            OnTriggerHold(lookValue);
        }
        
        public void ReleaseTrigger()
        {
            if (IsActive == false)
            {
                return;
            }
            
            IsPulled = false;
            WeaponContext.SetPointer(IsPulled);
            
            if (_isLoaded)
            {
                IsReload = false;
            }

            OnTriggerRelease();
        }

        protected virtual void OnTriggerPull()
        {
        }
        
        protected virtual void OnTriggerHold(Vector2 lookValue)
        {
        }

        protected virtual void OnTriggerRelease()
        {
        }

        public void TryReload()
        {
            if (IsActive && IsReload == false)
            {
                WeaponContext.StartCoroutine(Reload());
            }
        }
        
        protected void Shot()
        {
            if (IsReload)
            {
                return;
            }

            var contextTransform = WeaponContext.transform;
            var projectile = Object.Instantiate(
                _config.ProjectilePrefab, 
                contextTransform.position, 
                contextTransform.rotation);
            projectile.Initialize(_config);

            _projectilesCounter++;
            
            OnShot(projectile);

            if (_projectilesCounter >= _config.MagazineCapacity)
            {
                WeaponContext.StartCoroutine(Reload());
            }
        }

        protected virtual void OnShot(Projectile projectile)
        {
        }

        private IEnumerator Reload()
        {
            ReloadStarted?.Invoke(_index, _config.ReloadTime);
            
            _isLoaded = false;
            IsReload = true;

            yield return new WaitForSeconds(_config.ReloadTime);
            
            _projectilesCounter = 0;
            _isLoaded = true;
            IsReload = IsPulled;
        }
    }
}