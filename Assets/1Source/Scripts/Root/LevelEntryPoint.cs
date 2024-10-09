using EnemyCounterSystem;
using Modules.SavingsSystem;
using Scripts.Audio;
using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.CurrencySystem;
using Scripts.EnemySystem;
using Scripts.GameOverSystem;
using Scripts.GameStateSystem;
using Scripts.HealthSystem;
using Scripts.LevelSystem;
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
        [SerializeField] private PointerObserver _screenObserver;
        [SerializeField] private GameObject _enemiesContainer;
        [SerializeField] private EnemyStateHandler _enemyStateHandler;
        [SerializeField] private BulletCatcher _bulletCatcher;
        [SerializeField] private AudioButtonView _audioButtonView;

        protected override void OnDestroy()
        {
            Dispose();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            _audioButtonView.Init();

            builder.RegisterComponentInHierarchy<GameCanvasActivator>();

            builder.RegisterComponentInHierarchy<LevelLoader>();
            builder.RegisterComponentInHierarchy<LevelsView>();
            builder.RegisterComponentInHierarchy<LevelNumberView>();

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

            builder.RegisterComponentInHierarchy<PlayerHelthHandler>();
            builder.RegisterComponentInHierarchy<PlayerDieService>();

            builder.RegisterComponentInHierarchy<AudioButton>();
            builder.Register<AudioVolumeSaver>(Lifetime.Singleton);
            builder.Register<CurrencySaver>(Lifetime.Singleton);
            builder.Register<LevelSaver>(Lifetime.Singleton);

            ConfigureEnemyCounter(builder);

            builder.RegisterBuildCallback(container =>
            {
                container.InjectGameObject(_screenObserver.gameObject);
                container.InjectGameObject(_bulletCatcher.gameObject);
                container.Resolve<PlayerWeaponPresenter>();
                container.Resolve<AmmunitionPresenter>();
                container.Resolve<ReloadWeaponPresenter>();
                container.Resolve<AudioVolumeSaver>();
                container.Resolve<CurrencySaver>();
                container.Resolve<LevelSaver>();
            });
        }

        private void ConfigureEnemyCounter(IContainerBuilder builder)
        {
            Enemy[] enemies = _enemiesContainer.GetComponentsInChildren<Enemy>(true);
            EnemyCounter enemyCounter = new();
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