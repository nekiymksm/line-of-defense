using System.Collections;
using _project.Code.weapon.data.weapon.configs;
using _project.Code.weapon.weapons.Base;
using UnityEngine;

namespace _project.Code.weapon.weapons
{
    public class MachineGun : Weapon
    {
        private MachineGunConfig _machineGunConfig;
        private bool _canDoShot;
        
        public MachineGun(WeaponContext weaponContext, MachineGunConfig weaponConfig) 
            : base(weaponContext, weaponConfig)
        {
            _machineGunConfig = weaponConfig;
            _canDoShot = true;
        }

        protected override void OnTriggerHold()
        {
            if (IsPulled && IsReload == false && _canDoShot)
            {
                Shot();
                WeaponContext.StartCoroutine(DelayShot());
            }
        }

        private IEnumerator DelayShot()
        {
            _canDoShot = false;
            yield return new WaitForSeconds(_machineGunConfig.RateOfFire);
            _canDoShot = true;
        }
    }
}