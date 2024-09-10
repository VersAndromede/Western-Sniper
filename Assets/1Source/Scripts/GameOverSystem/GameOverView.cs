using DG.Tweening;
using Scripts.CameraSystem.CameraAimingSystem;
using UnityEngine;

namespace Scripts.GameOverSystem
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _container;
        [SerializeField] private float _duration;
        [SerializeField] private float _delay;
        [SerializeField] private AimingExitService _aimingExitService;
        [SerializeField] private CameraExitAimingHandler _cameraExitAimingHandler;

        public void Enable()
        {
            const int TargetFade = 1;

            _container.gameObject.SetActive(true);
            _container.DOFade(TargetFade, _duration).SetDelay(_delay);
            _cameraExitAimingHandler.CancelExit();
            _aimingExitService.Exit();
        }
    }
}