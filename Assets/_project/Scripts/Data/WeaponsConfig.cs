using System;
using _project.Scripts.ModuleTurret.Projectiles.Base;
using _project.Scripts.ModuleTurret.Weapons.Base;
using UnityEngine;

namespace _project.Scripts.Data
{
    public enum WeaponKind
    {
        MachineGun,
        RailGun,
        GrenadeLauncher
    }
    
    [CreateAssetMenu(fileName = "WeaponsConfig", menuName = "Configs/WeaponsConfig")]
    public class WeaponsConfig : ScriptableObject
    {
        [SerializeField] private WeaponConfig[] _weaponConfigs;

        public WeaponConfig Get(WeaponKind weaponKind)
        {
            for (int i = 0; i < _weaponConfigs.Length; i++)
            {
                if (_weaponConfigs[i].Kind == weaponKind)
                {
                    return _weaponConfigs[i];
                }
            }

            Debug.LogError("WEAPON_CONFIG_NOT_FOUND");
            return null;
        }
    }

    [Serializable]
    public class WeaponConfig
    {
        [SerializeField] private WeaponKind _kind;
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _rateOfFire;
        [SerializeField] private int _clipSize;

        public WeaponKind Kind => _kind;
        public Weapon WeaponPrefab => _weaponPrefab;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public float ReloadTime => _reloadTime;
        public float RateOfFire => _rateOfFire;
        public int ClipSize => _clipSize;
    }
}