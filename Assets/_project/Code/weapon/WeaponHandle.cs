using System.Collections.Generic;
using _project.Code.control.input;
using _project.Code.data.hold;
using _project.Code.session.state.@base;
using _project.Code.tower;
using _project.Code.weapon.data;
using _project.Code.weapon.enums;
using _project.Code.weapon.weapons;
using _project.Code.weapon.weapons.Base;

namespace _project.Code.weapon
{
    public class WeaponHandle : IStateObserver
    {
        private WeaponConfigsCollection _weaponsCollection;
        private SessionInputHandle _inputHandle;
        private DefenseHandle _defenseHandle;
        
        public List<Weapon> Weapons { get; private set; }

        public WeaponHandle(DataCollection dataCollection, SessionInputHandle inputHandle, DefenseHandle defenseHandle)
        {
            _weaponsCollection = dataCollection.GetData<WeaponConfigsCollection>();
            _inputHandle = inputHandle;
            _defenseHandle = defenseHandle;
        }

        public void OnStart()
        {
            Weapons = new List<Weapon>();
            var weaponSet = new []
            {
                WeaponKind.MachineGun, 
                WeaponKind.RailGun, 
                WeaponKind.GrenadeLauncher
            };
            
            for (int i = 0; i < weaponSet.Length; i++)
            {
                switch (weaponSet[i])
                {
                    case WeaponKind.MachineGun:
                        Weapons.Add(new MachineGun(
                            _weaponsCollection.MachineGunConfig, _defenseHandle.WeaponContext, i));
                        break;
                    
                    case WeaponKind.RailGun:
                        Weapons.Add(new RailGun( 
                            _weaponsCollection.RailGunConfig, _defenseHandle.WeaponContext, i));
                        break;
                    
                    case WeaponKind.GrenadeLauncher:
                        Weapons.Add(new GrenadeLauncher(
                            _weaponsCollection.GrenadeLauncherConfig, _defenseHandle.WeaponContext, i));
                        break;
                }
            }
            
            foreach (var weapon in Weapons)
            {
                _inputHandle.AddInputDependable(weapon);
            }
            
            ActivateWeapon(0);
        }

        public void OnPause()
        {
        }

        public void OnEnd()
        {
        }

        public void ActivateWeapon(int weaponIndex)
        {
            foreach (var weapon in Weapons)
            {
                weapon.TryReload();
            }
            
            if (weaponIndex >= Weapons.Count || weaponIndex < 0)
            {
                return;
            }
            
            for (int i = 0; i < Weapons.Count; i++)
            {
                Weapons[i].IsActive = i == weaponIndex;
            }
        }
    }
}