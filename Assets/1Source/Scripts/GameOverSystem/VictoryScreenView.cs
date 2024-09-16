using DG.Tweening;
using Scripts.CameraSystem.CameraAimingSystem;
using UnityEngine;

namespace Scripts.GameOverSystem
{
    public class VictoryScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _container;
        [SerializeField] private float _duration;
        [SerializeField] private float _delay;
        [SerializeField] private AimingExitService _aimingExitService;

        public void Enable()
        {
            const int TargetFade = 1;

            _container.gameObject.SetActive(true);
            _container.DOFade(TargetFade, _duration).SetDelay(_delay);
            _aimingExitService.Exit();
        }
    }
}