using System.Collections;
using _project.Code.tower;
using _project.Code.weapon.data.weapon.configs;
using _project.Code.weapon.weapons.Base;
using UnityEngine;

namespace _project.Code.weapon.weapons
{
    public class MachineGun : Weapon
    {
        private MachineGunConfig _machineGunConfig;
        private bool _canDoShot;
        
        public MachineGun(MachineGunConfig config, WeaponContext weaponContext, int index) 
            : base(config, weaponContext, index)
        {
            _machineGunConfig = config;
            _canDoShot = true;
        }

        protected override void OnTriggerHold(Vector2 lookValue)
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