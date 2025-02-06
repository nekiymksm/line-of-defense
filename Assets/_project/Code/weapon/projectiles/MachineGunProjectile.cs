using _project.Code.utility;
using _project.Code.weapon.projectiles.Base;
using UnityEngine;

namespace _project.Code.weapon.projectiles
{
    public class MachineGunProjectile : Projectile
    {
        private void Update()
        {
            transform.Translate(Vector3.forward * 50 * Time.deltaTime);
        }
        
        protected override void OnHit(IHittable enemy)
        {
            Destroy(gameObject);
            enemy.TakeHit(WeaponConfig.DamageValue);
        }
    }
}