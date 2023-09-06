using System.Collections;

namespace _project.Scripts.ModuleTurret.Utilities
{
    public interface IAutoShooting
    {
        public bool CanShoot { get; set; }

        public IEnumerator AutoShooting();
    }
}