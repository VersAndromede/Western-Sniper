﻿using Scripts.GameConfigSystem;
using Scripts.ShootingSystem;
using Scripts.UI;
using UnityEngine;

namespace Scripts.CameraSystem
{
    public class CameraExitAimingHandler : MonoBehaviour
    {
        [SerializeField] private CameraAiming _cameraAiming;

        private void Construct(PlayerWeapon playerWepon, GameConfig gameConfig)
        {
            
        }
    }

    public class CameraAimingHandler : MonoBehaviour
    {
        [SerializeField] private CameraAiming _cameraAiming;
        [SerializeField] private AnimatedButton _aimButton;
        [SerializeField] private AnimatedButton _exitAimingButton;
        [SerializeField] private AnimatedUI _aimPanel;
        [SerializeField] private GameObject _crosshairs;

        private void Start()
        {
            _cameraAiming.AimingInitiated += OnAimingInitiated;
            _cameraAiming.AimingCompleted += OnAimingCompleted;
            _cameraAiming.CameraReturned += OnCameraReturned;

            _aimButton.Down += OnAimButtonDown;
            _exitAimingButton.Down += OnExitAimingButtonDown;
        }

        private void OnDestroy()
        {
            _cameraAiming.AimingInitiated -= OnAimingInitiated;
            _cameraAiming.AimingCompleted -= OnAimingCompleted;
            _cameraAiming.CameraReturned -= OnCameraReturned;

            _aimButton.Down -= OnAimButtonDown;
            _exitAimingButton.Down -= OnExitAimingButtonDown;
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
            _aimButton.Unlock();
        }

        private void OnAimButtonDown()
        {
            _cameraAiming.StartAim(destroyCancellationToken);
        }

        private void OnExitAimingButtonDown()
        {
            _crosshairs.SetActive(true);
            _cameraAiming.EndAim(destroyCancellationToken);
            _aimButton.Show();
            _aimPanel.Hide();
            _exitAimingButton.Hide();
        }
    }
}