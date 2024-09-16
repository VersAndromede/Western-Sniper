using EnemyCounterSystem;
using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.CurrencySystem;
using Scripts.EnemySystem;
using Scripts.GameConfigSystem;
using Scripts.GameOverSystem;
using Scripts.GameStateSystem;
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
        [SerializeField] private EnemyStateHandler _enemyStateHandler;

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
            builder.Register<GameState>(Lifetime.Singleton);
            builder.Register<ObservingCameraSpeedFactory>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<PlayerWeaponView>();
            builder.Register<PlayerWeaponPresenter>(Lifetime.Singleton);

            builder.Register<Currency>(Lifetime.Singleton);
            builder.Register<AmountCurrencyPerLevel>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<AmountCurrencyPerLevelView>();
            builder.RegisterComponentInHierarchy<CurrencyAccrualView>();
            builder.RegisterComponentInHierarchy<ClaimButtonHandler>();

            builder.RegisterComponentInHierarchy<PointerObserverHandler>();
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

            builder.RegisterComponentInHierarchy<MainEnemyTargetTrigger>();
            builder.RegisterComponentInHierarchy<MainEnemy>();
            builder.Register<GameOver>(Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<VictoryScreenView>();
            builder.RegisterComponentInHierarchy<FailedScreenView>();
            builder.RegisterComponentInHierarchy<EnemyCounterHandler>();
            builder.Register<GameOverPresenter>(Lifetime.Singleton);

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<GameOverPresenter>();

                PlayerWeapon playerWeapon = container.Resolve<PlayerWeapon>();
                _enemyStateHandler.Init(enemies, playerWeapon);
            });
        }         
    }
}