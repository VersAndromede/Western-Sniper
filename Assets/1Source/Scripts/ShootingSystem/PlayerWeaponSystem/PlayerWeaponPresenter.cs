using Scripts.GameConfigSystem;
using Scripts.ShootingSystem.AmmunitionSystem;
using System;
using UnityEngine;
using VContainer;

namespace Scripts.ShootingSystem.PlayerWeaponSystem
{
    public class PlayerWeaponPresenter : IDisposable
    {
        private readonly PlayerWeapon _weapon;
        private readonly PlayerWeaponView  _weaponView;

        [Inject]
        public PlayerWeaponPresenter(PlayerWeapon weapon, PlayerWeaponView weaponView, Ammunition ammunition, GameConfig gameConfig)
        {
            _weapon = weapon;
            _weaponView = weaponView;
            _weapon.Init(ammunition, gameConfig);

            _weapon.ShotFired += OnShotFired;
        }

        public void Dispose()
        {
            _weapon.ShotFired -= OnShotFired;
        }

        private void OnShotFired(Vector3 hitPoint)
        {
            _weaponView.Play(hitPoint, _weapon.IsEmpty);
        }
    }
}