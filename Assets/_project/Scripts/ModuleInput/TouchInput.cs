using UnityEngine;

namespace _project.Scripts.ModuleInput
{
    public class TouchInput : MonoBehaviour
    {
        [SerializeField] private AimingInput _aimingInput;
        [SerializeField] private ChoosingWeaponInput _choosingWeaponInput;
        
        public AimingInput AimingInput => _aimingInput;
        public ChoosingWeaponInput ChoosingWeaponInput => _choosingWeaponInput;
    }
}