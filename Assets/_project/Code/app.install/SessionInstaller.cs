using _project.Code.enemy;
using _project.Code.session.result;
using _project.Code.session.state;
using _project.Code.session.state.enums;
using _project.Code.turret;
using _project.Code.weapon;
using UnityEngine;
using Zenject;

namespace _project.Code.app.install
{
    public class SessionInstaller : MonoInstaller
    {
        [SerializeField] private Camera _viewCamera;
        [SerializeField] private TurretTower _turretTower;
        [SerializeField] private EnemySpawner _enemySpawner;
        
        public override void InstallBindings()
        {
            Container.Bind<SessionStateHandle>().FromNew().AsSingle().NonLazy();
            Container.Bind<Camera>().FromInstance(_viewCamera).AsSingle().NonLazy();
            Container.Bind<WeaponsLoader>().FromNew().AsSingle().NonLazy();
            Container.Bind<TurretTower>().FromInstance(_turretTower).AsSingle().NonLazy();
            Container.Bind<EnemySpawner>().FromInstance(_enemySpawner).AsSingle().NonLazy();
            Container.Bind<SessionResultHandler>().FromNew().AsSingle().NonLazy();
        }

        public override void Start()
        {
            SetStateObservers();
            Container.Resolve<SessionStateHandle>().SetState(SessionStateKind.Start);
        }

        private void SetStateObservers()
        {
            var stateHandle = Container.Resolve<SessionStateHandle>();
            
            stateHandle.AddObserver(Container.Resolve<TurretTower>());
            stateHandle.AddObserver(Container.Resolve<EnemySpawner>());
        }
    }
}