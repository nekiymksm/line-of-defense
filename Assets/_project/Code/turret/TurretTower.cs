using System.Collections.Generic;
using _project.Code.control.input.enums;
using _project.Code.data.hold;
using _project.Code.session.result.data;
using _project.Code.session.state;
using _project.Code.session.state.@base;
using _project.Code.session.state.enums;
using _project.Code.turret.data;
using _project.Code.weapon;
using _project.Code.weapon.enums;
using _project.Code.weapon.weapons.Base;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace _project.Code.turret
{
    public class TurretTower : MonoBehaviour, IStateObserver
    {
        [SerializeField] private Transform _rotationAxisTransform;
        [SerializeField] private WeaponContext _weaponContext;
        
        [field:SerializeField] public DefenseArea DefenseArea { get; private set; }

        private DataHolder _dataHolder;
        private TurretConfig _turretConfig;
        private WeaponsLoader _weaponsLoader;
        private SessionStateHandle _sessionStateHandle;
        
        private int _weaponIndex;
        private Vector3 _rotationValue;
        private Vector2 _lookInputValue;
        private List<Weapon> _weapons;

        private InputAction _lookAction;
        private InputAction _aimAction;

        [Inject]
        private void Construct(DataHolder dataHolder, WeaponsLoader weaponsLoader, 
            SessionStateHandle sessionStateHandle)
        {
            _dataHolder = dataHolder;
            _weaponsLoader = weaponsLoader;
            _sessionStateHandle = sessionStateHandle;
        }

        private void Awake()
        {
            _rotationValue = Vector3.zero;
            _weaponIndex = 0;
            
            _lookAction = InputSystem.actions.FindAction(InputActionKind.TurretLook.ToString());
            _aimAction = InputSystem.actions.FindAction(InputActionKind.TurretShot.ToString());
            
            _turretConfig = _dataHolder.GetData<TurretConfig>();
        }

        private void Start()
        {
            var weaponsSet = new []{WeaponKind.MachineGun, WeaponKind.RailGun, WeaponKind.GrenadeLauncher};
            _weapons = _weaponsLoader.Load(weaponsSet, _weaponContext);

            DefenseArea.Initialize(
                _dataHolder.GetData<SessionConditionsConfig>().DefenseAreaHealth,
                () => _sessionStateHandle.SetState(SessionStateKind.End));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _weaponIndex++;

                if (_weaponIndex > 2)
                {
                    _weaponIndex = 0;
                }
            }
            
            if (_aimAction.IsPressed())
            {
                Aim();
                _weapons[_weaponIndex].HoldTrigger();
            }

            if (_aimAction.WasPressedThisFrame())
            {
                _weapons[_weaponIndex].PullTrigger();
            }
            
            if (_aimAction.WasReleasedThisFrame())
            {
                _weapons[_weaponIndex].ReleaseTrigger();
            }
        }

        private void Aim()
        {
            _lookInputValue = _lookAction.ReadValue<Vector2>();
            _rotationValue.y = _turretConfig.GetAngleValue(_lookInputValue.x);
            _rotationAxisTransform.rotation = Quaternion.Euler(_rotationValue);
        }
    }
}