using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameConfigSystem;
using Scripts.ShootingSystem.AmmunitionSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using Scripts.ShootingSystem.ReloadWeaponSystem;
using Scripts.ShootingSystem.ShotHandlerSystem;
using Scripts.UI;
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
            builder.RegisterInstance(_gameConfig);

            builder.Register<Ammunition>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<AmmunitionView>();
            builder.RegisterComponentInHierarchy<ReloadWeaponView>();
            builder.Register<ReloadWeaponPresenter>(Lifetime.Singleton);
            builder.Register<AmmunitionPresenter>(Lifetime.Singleton);

            builder.Register<PlayerWeapon>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<PlayerWeaponView>();
            builder.Register<PlayerWeaponPresenter>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<CameraLookingAtPoint>();
            builder.RegisterComponentInHierarchy<ShotHandler>();
            builder.RegisterComponentInHierarchy<CameraAimingHandler>();
            builder.RegisterComponentInHierarchy<AimingExitService>();
            builder.RegisterComponentInHierarchy<CameraExitAimingHandler>();
            builder.RegisterComponentInHierarchy<ExitButtonBlocker>();

            builder.RegisterBuildCallback(container =>
            {
                container.InjectGameObject(_screenObserver.gameObject);
                container.Resolve<PlayerWeaponPresenter>();
                container.Resolve<AmmunitionPresenter>();
                container.Resolve<ReloadWeaponPresenter>();
            });
        }
    }
}