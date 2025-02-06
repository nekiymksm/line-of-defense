using System.Collections.Generic;
using _project.Code.data.hold;
using _project.Code.weapon.data;
using _project.Code.weapon.enums;
using _project.Code.weapon.weapons.Base;

namespace _project.Code.weapon
{
    public class WeaponsLoader
    {
        private WeaponsCollection _weaponsCollection;
        
        public WeaponsLoader(DataHolder dataHolder)
        {
            _weaponsCollection = dataHolder.GetData<WeaponsCollection>();
        }
        
        public List<Weapon> Load(WeaponKind[] weaponSet, WeaponContext weaponContext)
        {
            var weapons = new List<Weapon>();

            foreach (var weaponKind in weaponSet)
            {
                if (_weaponsCollection.HasWeapon(weaponKind, weaponContext, out Weapon weapon))
                {
                    weapons.Add(weapon);
                }
            }

            return weapons;
        }
    }
}