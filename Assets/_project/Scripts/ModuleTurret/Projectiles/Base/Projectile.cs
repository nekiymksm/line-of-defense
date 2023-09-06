using System.Threading.Tasks;
using UnityEngine;

namespace _project.Scripts.ModuleTurret.Projectiles.Base
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] private int _lifeTime;

        private void Start()
        {
            Show();
            OnStart();
        }

        private async void Show()
        {
            await Task.Delay(_lifeTime);
            Destroy(gameObject);
        }

        protected virtual void OnStart()
        {
        }
    }
}