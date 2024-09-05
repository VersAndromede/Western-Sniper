using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameConfigSystem;
using Scripts.ShootingSystem;
using Scripts.ShootingSystem.AmmunitionSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using Scripts.ShootingSystem.ReloadWeaponSystem;
using Scripts.ShootingSystem.ShotHandlerSystem;
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
            builder.Register<Ammunition>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<AmmunitionView>();
            builder.RegisterComponentInHierarchy<ReloadWeaponView>();
            builder.Register<ReloadWeaponPresenter>(Lifetime.Singleton);
            builder.Register<AmmunitionPresenter>(Lifetime.Singleton);
            builder.Register<PlayerWeapon>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<PlayerWeaponView>();
            builder.Register<PlayerWeaponPresenter>(Lifetime.Singleton);

            builder.RegisterInstance(_gameConfig);
            builder.RegisterComponentInHierarchy<CameraLookingAtPoint>();
            builder.RegisterComponentInHierarchy<ShotHandler>();

            builder.RegisterBuildCallback(container =>
            {
                container.Inject(_screenObserver);
                container.Resolve<PlayerWeaponPresenter>();
                container.Resolve<AmmunitionPresenter>();
                container.Resolve<ReloadWeaponPresenter>();
            });
        }
    }
}