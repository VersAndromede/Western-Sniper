using Cysharp.Threading.Tasks;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.UI;
using UnityEngine;

namespace Scripts.CameraSystem.CameraAimingSystem
{
    public class AimingExitService : MonoBehaviour
    {
        [SerializeField] private PointerObserver _screenObserver;
        [SerializeField] private CameraAiming _cameraAiming;
        [SerializeField] private AnimatedUI _aimingScreen;

        public void Exit()
        {
            _screenObserver.ChangeType(PointerObserverType.ObserverScreen);
            _aimingScreen.Hide();
            _cameraAiming.EndAim();
        }

        public void HideButton(AnimatedButton animatedButton)
        {
            animatedButton.Lock();
            animatedButton.Hide();
        }

        public void ShowButton(AnimatedButton animatedButton)
        {
            animatedButton.Unlock();
            animatedButton.Show();
        }
    }
}