using System;
using System.Collections;
using _project.Scripts.Data;
using UnityEngine;

namespace _project.Scripts.ModuleTurret.Weapons.Base
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponKind _kind;
        [SerializeField] private WeaponsConfig _weaponsConfig;
        [SerializeField] private Transform _shotPointTransform;

        protected bool IsReload;
        protected WeaponConfig WeaponConfig;

        public float ReloadTime => WeaponConfig.ReloadTime;

        public event Action StartReload;

        private void Awake()
        {
            WeaponConfig = _weaponsConfig.Get(_kind);
            IsReload = false;
        }

        public virtual void OnPointerDown()
        {
        }

        public virtual void OnPointerUp()
        {
            if (IsReload == false)
            {
                Shot();
                StartCoroutine(Reload());
            }
        }
        
        protected void Shot()
        {
            Instantiate(WeaponConfig.ProjectilePrefab, _shotPointTransform.position, _shotPointTransform.rotation);
        }
        
        protected IEnumerator Reload()
        {
            StartReload?.Invoke();
            IsReload = true;
            yield return new WaitForSeconds(WeaponConfig.ReloadTime);
            IsReload = false;
        }
    }
}