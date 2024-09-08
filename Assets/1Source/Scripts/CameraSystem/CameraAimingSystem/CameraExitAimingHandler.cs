using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameConfigSystem;
using Scripts.ShootingSystem.AmmunitionSystem;
using Scripts.ShootingSystem.PlayerWeaponSystem;
using Scripts.UI;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
        private float _aimingExitTime;
        private bool _isReloading;

        private CancellationTokenSource _cancellationTokenSource = new();

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();

            _screenObserver.Down -= OnScreenDown;
            _exitAimingButton.Down -= OnExitButtonDown;

            _playerWeapon.ShotFired -= OnShotFired;
            _playerWeapon.Reloading -= OnReloading;
            _playerWeapon.Reloaded -= OnReloaded;
            _ammunition.Over -= OnOver;
        }

        [Inject]
        private void Construct(PlayerWeapon playerWeapon, Ammunition ammunition, GameConfig gameConfig)
        {
            _playerWeapon = playerWeapon;
            _ammunition = ammunition;
            _aimingExitTime = gameConfig.TimeToExitAiming;

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

            GetOutAiming(_cancellationTokenSource.Token, _aimingExitTime, () =>
            {
                _aimingExitService.HideButton(_exitAimingButton);
                _crosshairs.gameObject.SetActive(true);
                _aimingExitService.Exit();
            });
        }

        private void OnReloading()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            GetOutAiming(_cancellationTokenSource.Token, 0, () =>
            {
                if (_playerWeapon.IsEmpty == false)
                    _aimingExitService.HideButton(_aimButton);

                _aimingExitService.HideButton(_exitAimingButton);
                _aimingExitService.Exit();
                _crosshairs.gameObject.SetActive(true);
            });
        }

        private void OnReloaded()
        {
            _isReloading = false;
            _aimingExitService.ShowButton(_aimButton);
        }

        private async UniTask GetOutAiming(CancellationToken token, float delay, Action endWaitCallback)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), false, PlayerLoopTiming.Update, token);
            endWaitCallback();
        }
    }
}