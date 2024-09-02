using System;
using VContainer;

namespace Scripts.ShootingSystem
{
    public class PlayerWeaponPresenter : IDisposable
    {
        private readonly PlayerWeapon _weapon;
        private readonly PlayerWeaponView  _weaponView;

        [Inject]
        public PlayerWeaponPresenter(PlayerWeapon weapon, PlayerWeaponView weaponView)
        {
            _weapon = weapon;
            _weaponView = weaponView;

            _weapon.ShotFired += OnShotFired;
        }

        public void Dispose()
        {
            _weapon.ShotFired -= OnShotFired;
        }

        private void OnShotFired()
        {
            _weaponView.Play();
        }
    }
}