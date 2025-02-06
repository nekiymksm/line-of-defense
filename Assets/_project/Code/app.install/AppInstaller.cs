using _project.Code.data.hold;
using UnityEngine;
using Zenject;

namespace _project.Code.app.install
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField] private DataHolder dataHolder;

        public override void InstallBindings()
        {
            Container.Bind<DataHolder>().FromInstance(dataHolder).AsSingle().NonLazy();
        }
    }
}