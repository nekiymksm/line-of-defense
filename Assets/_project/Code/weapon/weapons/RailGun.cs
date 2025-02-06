using _project.Code.weapon.data.weapon.configs;
using _project.Code.weapon.weapons.Base;

namespace _project.Code.weapon.weapons
{
    public class RailGun : Weapon
    {
        public RailGun(WeaponContext weaponContext, RailGunConfig weaponConfig) 
            : base(weaponContext, weaponConfig)
        {
        }
        
        protected override void OnTriggerRelease()
        {
            Shot();
        }
    }
}