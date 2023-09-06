using System.Collections;
using _project.Scripts.ModuleTurret.Utilities;
using _project.Scripts.ModuleTurret.Weapons.Base;
using UnityEngine;

namespace _project.Scripts.ModuleTurret.Weapons
{
    public class MachineGun : Weapon, IAutoShooting
    {
        private int _shotsCounter;
        
        public bool CanShoot { get; set; }
        
        public override void OnPointerDown()
        {
            if (IsReload == false)
            {
                CanShoot = true;
                StartCoroutine(AutoShooting());
            }
        }

        public override void OnPointerUp()
        {
            if (IsReload == false)
            {
                StopCoroutine(AutoShooting());
                CanShoot = false;
            }
        }

        public IEnumerator AutoShooting()
        {
            while (CanShoot)
            {
                Shot();
                _shotsCounter++;
                
                yield return new WaitForSeconds(WeaponConfig.RateOfFire);

                if (_shotsCounter >= WeaponConfig.ClipSize)
                {
                    CanShoot = false;
                    _shotsCounter = 0;
                    StartCoroutine(Reload());
                }
            }
        }
    }
}