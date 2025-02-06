using _project.Code.utility;
using _project.Code.weapon.projectiles.Base;

namespace _project.Code.weapon.projectiles
{
    public class RailGunProjectile : Projectile
    {
        protected override void OnHit(IHittable enemy)
        {
            enemy.TakeHit(WeaponConfig.DamageValue);
        }
    }
}