using Cysharp.Threading.Tasks;
using Scripts.CameraSystem.Animations;
using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.EnemySystem;
using Scripts.GameOverSystem;
using Scripts.GameStateSystem;
using UnityEngine;
using VContainer;

namespace Scripts.HealthSystem
{
    public class PlayerDieService : MonoBehaviour
    {
        [SerializeField] private DieEffect _dieEffect;
        [SerializeField] private FailedScreen _screen;
        [SerializeField] private FailureCameraAnimation _failureCameraAnimation;
        [SerializeField] private CameraLookingAtPoint _cameraLookingAtPoint;
        [SerializeField] private AimingExitService _aimingExitService;
        [SerializeField] private float _animationDelay;

        private GameState _gameState;
        private EnemyAlgorithm[] _enemyAlgorithms;

        public async UniTask Play()
        {
            foreach (EnemyAlgorithm algorithm in _enemyAlgorithms)
                algorithm.Stop();

            _cameraLookingAtPoint.Disable();

            if (_gameState.Type == GameStateType.Aiming)
                await _aimingExitService.Exit();

            _dieEffect.Play();
            await UniTask.WaitForSeconds(_animationDelay, cancellationToken: destroyCancellationToken);
            _failureCameraAnimation.Animate();
            _screen.Show(_gameState.GameOverType);
        }

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;
            _enemyAlgorithms = GetComponentsInChildren<EnemyAlgorithm>(true);
        }
    }
}
