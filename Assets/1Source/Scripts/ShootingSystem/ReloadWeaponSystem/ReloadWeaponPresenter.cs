using System;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using VContainer;

namespace Scripts.ShootingSystem.ReloadWeaponSystem
{
    public class ReloadWeaponPresenter : IDisposable
    {
        private readonly PlayerWeapon _weapon;
        private readonly ReloadWeaponView _reloadWeaponView;

        [Inject]
        public ReloadWeaponPresenter(PlayerWeapon weapon, ReloadWeaponView reloadWeaponView)
        {
            _weapon = weapon;
            _reloadWeaponView = reloadWeaponView;

            _weapon.Reloading += OnReloading;
        }

        public void Dispose()
        {
            _weapon.Reloading -= OnReloading;
        }

        private void OnReloading() 
        {
            _reloadWeaponView.AnimateSlider();
        }
    }
}