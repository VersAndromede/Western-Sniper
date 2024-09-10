using EnemyCounterSystem;
using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.EnemySystem;
using Scripts.GameConfigSystem;
using Scripts.GameOverSystem;
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
        [SerializeField] private GameObject _enemiesContainer;

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

            ConfigureEnemyCounter(builder);

            builder.RegisterBuildCallback(container =>
            {
                container.InjectGameObject(_screenObserver.gameObject);
                container.Resolve<PlayerWeaponPresenter>();
                container.Resolve<AmmunitionPresenter>();
                container.Resolve<ReloadWeaponPresenter>();
            });
        }

        private void ConfigureEnemyCounter(IContainerBuilder builder)
        {
            Enemy[] enemies = _enemiesContainer.GetComponentsInChildren<Enemy>(true);
            EnemyCounter enemyCounter = new ();
            enemyCounter.Init(enemies);
            builder.RegisterInstance(enemyCounter);

            builder.RegisterComponentInHierarchy<MainEnemy>();
            builder.Register<GameOver>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<GameOverView>();
            builder.RegisterComponentInHierarchy<EnemyCounterHandler>();
            builder.Register<GameOverPresenter>(Lifetime.Singleton);

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<GameOverPresenter>();
            });
        }         
    }
}