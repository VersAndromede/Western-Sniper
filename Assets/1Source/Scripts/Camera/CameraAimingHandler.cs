using UnityEngine;

namespace Scripts.CameraSystem
{
    public class CameraAimingHandler : MonoBehaviour
    {
        [SerializeField] private CameraAiming _cameraAiming;
        [SerializeField] private AnimatedButton _aimButton;
        [SerializeField] private AnimatedButton _exitAimingButton;
        [SerializeField] private GameObject _crosshairs;

        private void Start()
        {
            _cameraAiming.Aimed += OnAimed;
            _aimButton.Down += OnAimButtonDown;
            _exitAimingButton.Down += OnExitAimingButtonDown;
        }

        private void OnDestroy()
        {
            _cameraAiming.Aimed -= OnAimed;
            _aimButton.Down -= OnAimButtonDown;
            _exitAimingButton.Down -= OnExitAimingButtonDown;
        }

        private void OnAimed()
        {
            _crosshairs.SetActive(false);
            _aimButton.Hide();
            _exitAimingButton.Show();
        }

        private void OnAimButtonDown()
        {
            _cameraAiming.StartAim();
        }

        private void OnExitAimingButtonDown()
        {
            _crosshairs.SetActive(true);
            _cameraAiming.EndAim();
            _aimButton.Show();
            _exitAimingButton.Hide();
        }
    }
}