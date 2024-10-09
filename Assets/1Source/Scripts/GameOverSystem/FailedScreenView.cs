using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.GameStateSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.CameraSystem.Animations;

namespace Scripts.GameOverSystem
{
    public class FailedScreenView : MonoBehaviour
    {
        [SerializeField] private FailedScreen _screen;
        [SerializeField] private float _lockDelay;
        [SerializeField] private float _animationDelay;
        [SerializeField] private GameObject[] _objectsForDeactivate;
        [SerializeField] private GameObject _aimingButton;
        [SerializeField] private AimingExitService _aimingExitService;
        [SerializeField] private FailureCameraAnimation _failureCameraAnimation;

        private GameState _gameState;

        public async UniTask Enable()
        {
            await UniTask.WaitForSeconds(_lockDelay, cancellationToken: destroyCancellationToken);

            if (_gameState.Type == GameStateType.Aiming)
                await _aimingExitService.Exit();

            await UniTask.WaitForSeconds(_animationDelay, cancellationToken: destroyCancellationToken);

            foreach (GameObject objectForDeactivate in _objectsForDeactivate)
                _aimingExitService.Deactivate(objectForDeactivate);

            _screen.Show(_gameState.GameOverType);
            _failureCameraAnimation.Animate();
        }

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
        }
    }
}