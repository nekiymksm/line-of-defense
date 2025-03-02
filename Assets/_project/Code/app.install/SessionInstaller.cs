using _project.Code.control.input;
using _project.Code.enemy;
using _project.Code.session.result;
using _project.Code.session.state;
using _project.Code.session.state.enums;
using _project.Code.tower;
using _project.Code.ui;
using _project.Code.ui.elements.weapon.control.panel;
using _project.Code.ui.enums;
using _project.Code.weapon;
using UnityEngine;
using Zenject;

namespace _project.Code.app.install
{
    public class SessionInstaller : MonoInstaller
    {
        [SerializeField] private Camera _viewCamera;
        [SerializeField] private SessionInputHandle _sessionInputHandle;
        [SerializeField] private DefenseHandle defenseHandle;
        [SerializeField] private EnemySpawnHandle enemySpawnHandle;
        [SerializeField] private UiHandle _uiHandle;
        
        public override void InstallBindings()
        {
            Container.Bind<SessionStateHandle>().FromNew().AsSingle().NonLazy();
            Container.Bind<SessionInputHandle>().FromInstance(_sessionInputHandle).NonLazy();
            Container.Bind<Camera>().FromInstance(_viewCamera).AsSingle().NonLazy();
            Container.Bind<WeaponHandle>().FromNew().AsSingle().NonLazy();
            Container.Bind<DefenseHandle>().FromInstance(defenseHandle).AsSingle().NonLazy();
            Container.Bind<EnemySpawnHandle>().FromInstance(enemySpawnHandle).AsSingle().NonLazy();
            Container.Bind<SessionResultsHandle>().FromNew().AsSingle().NonLazy();
            Container.Bind<UiHandle>().FromInstance(_uiHandle).AsSingle().NonLazy();
        }

        public override void Start()
        {
            SetStateObservers();
            Container.Resolve<UiHandle>().CanShow(CanvasKind.Main, out WeaponSwitchPanel weaponSwitchPanel);
        }

        private void SetStateObservers()
        {
            var stateHandle = Container.Resolve<SessionStateHandle>();
            
            stateHandle.AddObserver(Container.Resolve<EnemySpawnHandle>());
            stateHandle.AddObserver(Container.Resolve<SessionInputHandle>());
            stateHandle.AddObserver(Container.Resolve<SessionResultsHandle>());
            stateHandle.AddObserver(Container.Resolve<WeaponHandle>());
            
            Container.Resolve<SessionStateHandle>().SetState(SessionStateKind.Start);
        }
    }
}