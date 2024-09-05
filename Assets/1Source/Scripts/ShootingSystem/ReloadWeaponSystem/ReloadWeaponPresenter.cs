using Scripts.ShootingSystem.AmmunitionSystem;
using System;
using VContainer;

namespace Scripts.ShootingSystem.ReloadWeaponSystem
{
    public class ReloadWeaponPresenter : IDisposable
    {
        private readonly Ammunition _ammunition;
        private readonly ReloadWeaponView _reloadWeaponView;

        [Inject]
        public ReloadWeaponPresenter(Ammunition ammunition, ReloadWeaponView reloadWeaponView)
        {
            _ammunition = ammunition;
            _reloadWeaponView = reloadWeaponView;

            _ammunition.Over += OnOver;
        }

        public void Dispose()
        {
            _ammunition.Over -= OnOver;
        }

        private void OnOver() 
        {
            _reloadWeaponView.AnimateSlider();
        }
    }
}