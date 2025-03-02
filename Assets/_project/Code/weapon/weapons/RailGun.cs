using _project.Code.tower;
using _project.Code.weapon.data.weapon.configs;
using _project.Code.weapon.weapons.Base;

namespace _project.Code.weapon.weapons
{
    public class RailGun : Weapon
    {
        public RailGun(RailGunConfig config, WeaponContext weaponContext, int index) 
            : base(config, weaponContext, index)
        {
        }
        
        protected override void OnTriggerRelease()
        {
            Shot();
        }
    }
}