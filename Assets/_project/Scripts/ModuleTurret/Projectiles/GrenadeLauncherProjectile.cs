using _project.Scripts.ModuleTurret.Projectiles.Base;
using UnityEngine;

namespace _project.Scripts.ModuleTurret.Projectiles
{
    public class GrenadeLauncherProjectile : Projectile
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _launchForce;
        
        protected override void OnStart()
        {
            _rigidbody.AddForce(transform.forward * _launchForce, ForceMode.Impulse);
        }
    }
}