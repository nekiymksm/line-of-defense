using _project.Scripts.ModuleTurret.Projectiles.Base;
using UnityEngine;

namespace _project.Scripts.ModuleTurret.Projectiles
{
    public class MachineGunProjectile : Projectile
    {
        [SerializeField] private float _moveSpeed;

        private void Update()
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
    }
}