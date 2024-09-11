using System;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameStateSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using VContainer;

namespace Scripts.ShootingSystem.ReloadWeaponSystem
{
    public class ReloadWeaponPresenter : IDisposable
    {
        private readonly PlayerWeapon _weapon;
        private readonly ReloadWeaponView _reloadWeaponView;
        private readonly GameState _gameState;

        [Inject]
        public ReloadWeaponPresenter(PlayerWeapon weapon, ReloadWeaponView reloadWeaponView, GameState gameState)
        {
            _weapon = weapon;
            _reloadWeaponView = reloadWeaponView;
            _gameState = gameState;

            _weapon.Reloading += OnReloading;
        }

        public void Dispose()
        {
            _weapon.Reloading -= OnReloading;
        }

        private void OnReloading()
        {
            if (_gameState.Type != GameStateType.Over)
                _reloadWeaponView.AnimateSlider();
        }
    }
}