using System;
using UnityEngine;
using Scripts.CameraSystem.CameraAimingSystem;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Scripts.GameStateSystem;
using VContainer;
using Scripts.CameraSystem.PointerObserverSystem;

namespace Scripts.GameOverSystem
{
    public class FailedScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _container;
        [SerializeField] private float _duration;
        [SerializeField] private float _lockDelay;
        [SerializeField] private float _animationDelay;
        [SerializeField] private GameObject[] _objectsForDeactivate;
        [SerializeField] private GameObject _aimingButton;
        [SerializeField] private AimingExitService _aimingExitService;
        [SerializeField] private FailureCameraAnimation _failureCameraAnimation;

        private GameState _gameState;

        public async UniTask Enable()
        {
            const int TargetFade = 1;

            await UniTask.Delay(TimeSpan.FromSeconds(_lockDelay), false, PlayerLoopTiming.Update, destroyCancellationToken);
            _container.gameObject.SetActive(true);

            if (_gameState.Type == GameStateType.Aiming)
                await _aimingExitService.Exit();

            await UniTask.WaitForSeconds(_animationDelay, cancellationToken: destroyCancellationToken);

            foreach (GameObject objectForDeactivate in _objectsForDeactivate)
                _aimingExitService.Deactivate(objectForDeactivate);
            
            _container.DOFade(TargetFade, _duration);
            _failureCameraAnimation.Animate();
        }

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }
    }
}