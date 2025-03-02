using _project.Code.data.hold;
using UnityEngine;
using Zenject;

namespace _project.Code.app.install
{
    public class AppInstaller : MonoInstaller
    {
        [SerializeField] private DataCollection _dataCollection;

        public override void InstallBindings()
        {
            Container.Bind<DataCollection>().FromScriptableObject(_dataCollection).AsSingle().NonLazy();
        }
    }
}