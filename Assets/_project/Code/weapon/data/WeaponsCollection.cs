using _project.Code.weapon.data.weapon.configs;
using _project.Code.weapon.enums;
using _project.Code.weapon.weapons;
using _project.Code.weapon.weapons.Base;
using UnityEngine;

namespace _project.Code.weapon.data
{
    [CreateAssetMenu(fileName = "WeaponsCollection", menuName = "Data/Weapons/WeaponsCollection")]
    public class WeaponsCollection : ScriptableObject
    {
        [SerializeField] private MachineGunConfig _machineGunConfig;
        [SerializeField] private RailGunConfig _railGunConfig;
        [SerializeField] private GrenadeLauncherConfig _grenadeLauncherConfig;

        public bool HasWeapon(WeaponKind weaponKind, WeaponContext weaponContext, out Weapon weapon)
        {
            switch (weaponKind)
            {
                case WeaponKind.MachineGun:
                    weapon = new MachineGun(weaponContext, _machineGunConfig);
                    return true;

                case WeaponKind.RailGun:
                    weapon = new RailGun(weaponContext, _railGunConfig);
                    return true;

                case WeaponKind.GrenadeLauncher:
                    weapon = new GrenadeLauncher(weaponContext, _grenadeLauncherConfig);
                    return true;
            }

            weapon = null;
            return false;
        }
    }
}