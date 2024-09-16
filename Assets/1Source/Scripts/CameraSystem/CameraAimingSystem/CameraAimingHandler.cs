using Scripts.ShootingSystem.PlayerWeaponSystem;
using Scripts.UI;
using TMPro;
using UnityEngine;
using VContainer;

namespace Scripts.CameraSystem.CameraAimingSystem
{
    public class CameraAimingHandler : MonoBehaviour
    {
        [SerializeField] private CameraAiming _cameraAiming;
        [SerializeField] private AnimatedButton _aimButton;
        [SerializeField] private AnimatedButton _exitAimingButton;
        [SerializeField] private AnimatedUI _aimPanel;
        [SerializeField] private GameObject _crosshairs;

        private PlayerWeapon _playerWeapon;

        private void OnDestroy()
        {
            _cameraAiming.AimingInitiated -= OnAimingInitiated;
            _cameraAiming.AimingCompleted -= OnAimingCompleted;
            _cameraAiming.CameraReturned -= OnCameraReturned;

            _aimButton.Down -= OnAimButtonDown;
            _exitAimingButton.Down -= OnExitAimingButtonDown;
        }

        [Inject]
        private void Construct(PlayerWeapon playerWeapon)
        {
            _playerWeapon = playerWeapon;

            _cameraAiming.AimingInitiated += OnAimingInitiated;
            _cameraAiming.AimingCompleted += OnAimingCompleted;
            _cameraAiming.CameraReturned += OnCameraReturned;

            _aimButton.Down += OnAimButtonDown;
            _exitAimingButton.Down += OnExitAimingButtonDown;
        }

        private void OnAimingInitiated()
        {
            _crosshairs.SetActive(false);
            _aimButton.Hide();
            _aimPanel.Show();
            _exitAimingButton.Show();
        }

        private void OnAimingCompleted()
        {
            _exitAimingButton.Unlock();
        }

        private void OnCameraReturned()
        {
            if (_playerWeapon.IsEmpty == false)
            {
                _aimButton.Unlock();
                _aimButton.Show();
            }
        }

        private void OnAimButtonDown()
        {
            _cameraAiming.StartAim();
        }

        private void OnExitAimingButtonDown()
        {
            _crosshairs.SetActive(true);
            _cameraAiming.EndAim();
            _aimPanel.Hide();
            _exitAimingButton.Hide();
        }
    }
}