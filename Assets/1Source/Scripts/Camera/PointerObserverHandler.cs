using Scripts.UI;
using UnityEngine;

namespace Scripts.CameraSystem
{
    public class PointerObserverHandler : MonoBehaviour
    {
        [SerializeField] private PointerObserver _screenObserver;
        [SerializeField] private PointerObserver _aimButton;
        [SerializeField] private PressedButton _exitAimingButton;

        private void Start()
        {
            _aimButton.DragStarted += OnDragStarted;
            _exitAimingButton.Down += OnDown;
        }

        private void OnDestroy()
        {
            _aimButton.DragStarted -= OnDragStarted;
            _exitAimingButton.Down -= OnDown;
        }

        private void OnDragStarted()
        {
            _screenObserver.ChangeType(PointerObserverType.AimButton);
        }

        private void OnDown()
        {
            _screenObserver.ChangeType(PointerObserverType.ObserverScreen);
        }
    }
}
