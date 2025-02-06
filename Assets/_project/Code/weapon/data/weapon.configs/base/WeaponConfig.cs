using _project.Code.weapon.projectiles.Base;

namespace _project.Code.weapon.data.weapon.configs.Base
{
    public abstract class WeaponConfig
    {
        public Projectile ProjectilePrefab;
        public float ProjectileLifetime;
        public int MagazineCapacity;
        public float ReloadTime;
        public int DamageValue;
    }
}