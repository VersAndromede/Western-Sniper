using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameStateSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using UnityEngine;
using VContainer;

namespace Scripts.UI
{
    public class ExitButtonBlocker : MonoBehaviour
    {
        [SerializeField] private AnimatedButton _exitAimingButton;

        private PlayerWeapon _playerWeapon;
        private GameState _gameState;

        private void OnDestroy()
        {
            _playerWeapon.ShotFired -= OnShotFired;
            _playerWeapon.BulletInserted -= OnInsertedBullet;
            _playerWeapon.Reloading -= OnReloading;
        }

        [Inject]
        private void Construct(PlayerWeapon playerWeapon, GameState gameState)
        {
            _playerWeapon = playerWeapon;
            _gameState = gameState;

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
            if (_gameState.Type == GameStateType.Over)
                return;

            _exitAimingButton.gameObject.SetActive(true);
        }

        private void OnReloading()
        {
            if (_gameState.Type == GameStateType.Over)
                return;

            _exitAimingButton.Lock();
            _exitAimingButton.gameObject.SetActive(true);
        }
    }
}