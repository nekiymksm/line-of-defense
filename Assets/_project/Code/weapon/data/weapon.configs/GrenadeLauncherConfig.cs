using System;
using _project.Code.weapon.data.weapon.configs.Base;

namespace _project.Code.weapon.data.weapon.configs
{
    [Serializable]
    public class GrenadeLauncherConfig : WeaponConfig
    {
        public float ForceIntakeRate;
        public float MaxLaunchForce;
        public float MinLaunchForce;
        public float ExplosionLifetime;
    }
}