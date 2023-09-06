using _project.Scripts.ModuleInput;
using UnityEngine;
using Zenject;

namespace _project.Scripts
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private TouchInput _touchInput;
        
        public override void InstallBindings()
        {
            Container.Bind<TouchInput>().FromInstance(_touchInput).AsSingle();
        }
    }
}