using System.Collections.Generic;
using _project.Scripts.Data;
using _project.Scripts.ModuleInput;
using _project.Scripts.ModuleTurret.Weapons.Base;
using UnityEngine;
using Zenject;

namespace _project.Scripts.ModuleTurret
{
    public class TurretTower : MonoBehaviour
    {
        [SerializeField] private WeaponsConfig _weaponsConfig;
        [SerializeField] private Transform _pointerTransform;
        [SerializeField] private Transform _towerTransform;
        [SerializeField] private Transform _rotationAxisTransform;
        [SerializeField] private Transform _weaponsContainerTransform;
        [SerializeField] private float _rotationAngleValue;
        
        private TouchInput _touchInput;
        private int _currentWeaponIndex;
        private List<Weapon> _equippedWeapons;

        [Inject]
        private void Construct(TouchInput touchInput)
        {
            _touchInput = touchInput;
            _equippedWeapons = new List<Weapon>();
        }

        private void Start()
        {
            _pointerTransform.gameObject.SetActive(false);

            _touchInput.AimingInput.DownPointer += OnPointerDown;
            _touchInput.AimingInput.UpPointer += OnPointerUp;
            _touchInput.ChoosingWeaponInput.ButtonChosen += ChooseWeapon;

            var weapons = new List<WeaponKind>();
            weapons.Add(WeaponKind.MachineGun);
            weapons.Add(WeaponKind.GrenadeLauncher);
            weapons.Add(WeaponKind.RailGun);
            EquipWeapons(weapons);
        }

        private void Update()
        {
            if (_touchInput.AimingInput.PointerHorizontalValue != 0)
            {
                _towerTransform.rotation = 
                    Quaternion.Euler(Vector3.up * _touchInput.AimingInput.PointerHorizontalValue * _rotationAngleValue);
                _rotationAxisTransform.rotation = _towerTransform.rotation;
            }
        }
        
        private void OnDestroy()
        {
            _touchInput.AimingInput.DownPointer -= OnPointerDown;
            _touchInput.AimingInput.UpPointer -= OnPointerUp;
            _touchInput.ChoosingWeaponInput.ButtonChosen -= ChooseWeapon;
        }

        public void EquipWeapons(List<WeaponKind> weapons)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                _equippedWeapons.Add(Instantiate(_weaponsConfig.Get(weapons[i]).WeaponPrefab, _weaponsContainerTransform));
                _equippedWeapons[i].StartReload += ShowReload;
            }
        }

        public void UnEquipWeapons()
        {
            for (int i = 0; i < _equippedWeapons.Count; i++)
            {
                _equippedWeapons[i].StartReload -= ShowReload;
                Destroy(_equippedWeapons[i]);
            }
            
            _equippedWeapons.Clear();
        }

        private void OnPointerDown()
        {
            _pointerTransform.gameObject.SetActive(true);
            _equippedWeapons[_currentWeaponIndex].OnPointerDown();
        }
    
        private void OnPointerUp()
        {
            _pointerTransform.gameObject.SetActive(false);
            _equippedWeapons[_currentWeaponIndex].OnPointerUp();
        }
        
        private void ChooseWeapon(int weaponIndex)
        {
            _currentWeaponIndex = weaponIndex;
        }

        private void ShowReload()
        {
            _touchInput.ChoosingWeaponInput.ShowReload(_currentWeaponIndex, _equippedWeapons[_currentWeaponIndex].ReloadTime);
        }
    }
}