using Cysharp.Threading.Tasks;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameStateSystem;
using System;
using System.Threading;
using UnityEngine;
using VContainer;

namespace Scripts.EnemySystem
{
    public class BulletCatcher : MonoBehaviour
    {
        [SerializeField] private bool _isDamagingPlayer;
        [SerializeField] private float _invulnerabilityTime;

        private GameState _gameState;
        private CancellationTokenSource _cancellationToken = new();
        private bool _isInvulnerable = true;

        public event Action Catched;

        private void OnDestroy()
        {
            _cancellationToken.Cancel();

            if (_gameState != null)
                _gameState.Changed -= OnChanged;
        }

        public void Catch()
        {
            if (IsCanTakeDamage())
            {
                Catched?.Invoke();
                _isInvulnerable = true;
                DisableInvulnerable();
            }
        }

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;

            _gameState.Changed += OnChanged;
        }

        private async UniTask DisableInvulnerable()
        {
            await UniTask.WaitForSeconds(_invulnerabilityTime, cancellationToken: _cancellationToken.Token);
            _isInvulnerable = false;
        }

        private bool IsCanTakeDamage()
        {
            return _isDamagingPlayer
                && _isInvulnerable == false
                && _gameState.IsGameOver == false
                && _gameState.Type == GameStateType.Aiming;
        }

        private void OnChanged()
        {
            if (_gameState.Type == GameStateType.Aiming)
            {
                DisableInvulnerable();
                return;
            }

            _cancellationToken.Cancel();
            _cancellationToken = new CancellationTokenSource();
            _isInvulnerable = true;
        }
    }
}
