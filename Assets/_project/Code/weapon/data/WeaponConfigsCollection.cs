using _project.Code.weapon.data.weapon.configs;
using UnityEngine;

namespace _project.Code.weapon.data
{
    [CreateAssetMenu(fileName = "WeaponConfigsCollection", menuName = "Data/Weapons/WeaponConfigsCollection")]
    public class WeaponConfigsCollection : ScriptableObject
    {
        [field: SerializeField] public MachineGunConfig MachineGunConfig { get; private set; }
        [field: SerializeField] public RailGunConfig RailGunConfig { get; private set; }
        [field: SerializeField] public GrenadeLauncherConfig GrenadeLauncherConfig { get; private set; }
    }
}