using Scripts.CameraSystem;
using Scripts.ShootingSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Root
{
    public class LevelEntryPoint : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PointerObserver _screenObserver;

        protected override void OnDestroy()
        {
            Dispose();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerWeapon>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<PlayerWeaponView>();
            builder.Register<PlayerWeaponPresenter>(Lifetime.Singleton);

            builder.RegisterInstance(_gameConfig);
            builder.RegisterComponentInHierarchy<CameraObserver>();
            builder.RegisterComponentInHierarchy<ShotHandler>();

            builder.RegisterBuildCallback(container =>
            {
                container.Inject(_screenObserver);
                container.Resolve<PlayerWeaponPresenter>();
            });
        }
    }
}