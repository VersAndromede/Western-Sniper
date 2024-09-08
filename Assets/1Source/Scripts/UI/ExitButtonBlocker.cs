using Scripts.ShootingSystem.PlayerWeaponSystem;
using UnityEngine;
using VContainer;

namespace Scripts.UI
{
    public class ExitButtonBlocker : MonoBehaviour
    {
        [SerializeField] private AnimatedButton _exitAimingButton;

        private PlayerWeapon _playerWeapon;

        private void OnDestroy()
        {
            _playerWeapon.ShotFired -= OnShotFired;
            _playerWeapon.BulletInserted -= OnInsertedBullet;
            _playerWeapon.Reloading -= OnReloading;
        }

        [Inject]
        private void Construct(PlayerWeapon playerWeapon)
        {
            _playerWeapon = playerWeapon;

            _playerWeapon.ShotFired += OnShotFired;
            _playerWeapon.BulletInserted += OnInsertedBullet;
            _playerWeapon.Reloading += OnReloading;
        }

        private void OnShotFired(Vector3 _)
        {
            _exitAimingButton.gameObject.SetActive(false);
        }

        private void OnInsertedBullet()
        {
            _exitAimingButton.gameObject.SetActive(true);
        }

        private void OnReloading()
        {
            _exitAimingButton.Lock();
            _exitAimingButton.gameObject.SetActive(true);
        }
    }
}