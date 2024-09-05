using Scripts.UI;
using UnityEngine;

namespace Scripts.CameraSystem.PointerObserverSystem
{
    public class PointerObserverHandler : MonoBehaviour
    {
        [SerializeField] private PointerObserver _screenObserver;
        [SerializeField] private PressedButton _aimButton;
        [SerializeField] private PressedButton _exitAimingButton;

        private void Start()
        {
            _aimButton.Down += OnAimButtonDown;
            _exitAimingButton.Down += OnExitButtonDown;
        }

        private void OnDestroy()
        {
            _aimButton.Down -= OnAimButtonDown;
            _exitAimingButton.Down -= OnExitButtonDown;
        }

        private void OnAimButtonDown()
        {
            _screenObserver.ChangeType(PointerObserverType.AimButton);
        }

        private void OnExitButtonDown()
        {
            _screenObserver.ChangeType(PointerObserverType.ObserverScreen);
        }
    }
}
