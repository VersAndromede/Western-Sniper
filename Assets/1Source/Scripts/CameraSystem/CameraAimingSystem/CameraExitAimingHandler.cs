using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameConfigSystem;
using Scripts.GameStateSystem;
using Scripts.ShootingSystem.AmmunitionSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using Scripts.UI;
using UnityEngine;
using VContainer;

namespace Scripts.CameraSystem.CameraAimingSystem
{
    public class CameraExitAimingHandler : MonoBehaviour
    {
        [SerializeField] private AimingExitService _aimingExitService;
        [SerializeField] private AnimatedButton _exitAimingButton;
        [SerializeField] private AnimatedButton _aimButton;
        [SerializeField] private PointerObserver _screenObserver;
        [SerializeField] private GameObject _crosshairs;

        private PlayerWeapon _playerWeapon;
        private Ammunition _ammunition;
        private GameState _gameState;
        private float _aimingExitTime;
        private bool _isReloading;

        private CancellationTokenSource _cancellationTokenSource = new();

        private void OnDestroy()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource.Cancel();

            _screenObserver.Down -= OnScreenDown;
            _exitAimingButton.Down -= OnExitButtonDown;

            _playerWeapon.ShotFired -= OnShotFired;
            _playerWeapon.Reloading -= OnReloading;
            _playerWeapon.Reloaded -= OnReloaded;
            _ammunition.Over -= OnOver;
        }

        [Inject]
        private void Construct(PlayerWeapon playerWeapon, Ammunition ammunition, GameConfig gameConfig, GameState gameState)
        {
            _playerWeapon = playerWeapon;
            _ammunition = ammunition;
            _aimingExitTime = gameConfig.TimeToExitAiming;
            _gameState = gameState;

            _screenObserver.Down += OnScreenDown;
            _exitAimingButton.Down += OnExitButtonDown;

            _playerWeapon.ShotFired += OnShotFired;
            _playerWeapon.Reloading += OnReloading;
            _playerWeapon.Reloaded += OnReloaded;
            _ammunition.Over += OnOver;
        }

        private void OnScreenDown()
        {
            if (_isReloading == false)
                _cancellationTokenSource.Cancel();
        }

        private void OnExitButtonDown()
        {
            _cancellationTokenSource.Cancel();
        }

        private void OnOver()
        {
            _isReloading = true;
        }

        private void OnShotFired(Vector3 _)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            GetOutAiming(_cancellationTokenSource.Token, _aimingExitTime);
        }

        private void OnReloading()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            GetOutAiming(_cancellationTokenSource.Token, 0);
        }

        private void OnReloaded()
        {
            _isReloading = false;
            _aimingExitService.ShowButton(_aimButton);
        }

        private async UniTask GetOutAiming(CancellationToken token, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), false, PlayerLoopTiming.Update, token);

            if (_gameState.Type == GameStateType.Over)
                return;

            _aimingExitService.HideButton(_exitAimingButton);
            _crosshairs.SetActive(true);
            _gameState.ChangeType(GameStateType.Observation);
            _aimingExitService.Exit();
        }
    }
}