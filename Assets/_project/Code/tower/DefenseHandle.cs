using _project.Code.control.input;
using _project.Code.control.input.@base;
using _project.Code.data.hold;
using _project.Code.session.result.data;
using _project.Code.session.state;
using _project.Code.session.state.enums;
using _project.Code.tower.data;
using UnityEngine;
using Zenject;

namespace _project.Code.tower
{
    public class DefenseHandle : MonoBehaviour, IInputDependable
    {
        [SerializeField] private Transform[] _rotationAxisTransforms;
        
        [field: SerializeField] public WeaponContext WeaponContext { get; private set; }
        [field: SerializeField] public DefenseArea DefenseArea { get; private set; }
        
        private SessionInputHandle _sessionInputHandle;
        private SessionStateHandle _sessionStateHandle;
        private TurretConfig _turretConfig;
        private SessionConditionsConfig _sessionConditionsConfig;
        private Vector3 _rotationValue;

        [Inject]
        private void Construct(DataCollection dataCollection, SessionStateHandle sessionStateHandle, 
            SessionInputHandle sessionInputHandle)
        {
            _sessionInputHandle = sessionInputHandle;
            _sessionStateHandle = sessionStateHandle;
            _turretConfig = dataCollection.GetData<TurretConfig>();
            _sessionConditionsConfig = dataCollection.GetData<SessionConditionsConfig>();
        }

        private void Start()
        {
            _sessionInputHandle.AddInputDependable(this);
            
            DefenseArea.Initialize(
                _sessionConditionsConfig.DefenseAreaHealth, 
                () => _sessionStateHandle.SetState(SessionStateKind.End));
        }

        public void PullTrigger()
        {
        }

        public void HoldTrigger(Vector2 lookValue)
        {
            _rotationValue.y = _turretConfig.GetAngleValue(lookValue.x);
            
            foreach (var axis in _rotationAxisTransforms)
            {
                axis.localRotation = Quaternion.Euler(_rotationValue);
            }
        }

        public void ReleaseTrigger()
        {
        }
    }
}