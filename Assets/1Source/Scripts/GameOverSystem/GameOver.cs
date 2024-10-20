using System;
using Scripts.EnemySystem;
using Scripts.GameStateSystem;
using VContainer;
using Scripts.HealthSystem;
using Scripts.EnemySystem.EnemyCounterSystem;

namespace Scripts.GameOverSystem
{
    public class GameOver
    {
        private readonly MainEnemy _mainTarget;
        private readonly EnemyCounter _enemyCounter;
        private readonly MainEnemyTargetTrigger _mainEnemyTargetTrigger;
        private readonly PlayerHelthHandler _playerHelthHandler;
        private readonly GameState _gameState;

        private bool _allEnemiesDied;
        private bool _mainTargetDied;

        public event Action<GameOverType> Invoked;

        [Inject]
        public GameOver(EnemyCounter enemyCounter, MainEnemy mainEnemy, PlayerHelthHandler playerHelthHandler, GameState gameState)
        {
            _mainEnemyTargetTrigger = UnityEngine.Object.FindAnyObjectByType<MainEnemyTargetTrigger>();

            _enemyCounter = enemyCounter;
            _mainTarget = mainEnemy;
            _playerHelthHandler = playerHelthHandler;
            _gameState = gameState;

            _enemyCounter.AllDied += OnAllEnemyDied;
            _mainTarget.Died += OnTargetDied;
            _playerHelthHandler.Died += OnPlayerDied;

            if (_mainEnemyTargetTrigger != null)
                _mainEnemyTargetTrigger.EnemyReached += OnEnemyReached;
        }

        public void Unsubscribe()
        {
            _enemyCounter.AllDied -= OnAllEnemyDied;
            _mainTarget.Died -= OnTargetDied;
            _playerHelthHandler.Died -= OnPlayerDied;

            if (_mainEnemyTargetTrigger != null)
                _mainEnemyTargetTrigger.EnemyReached -= OnEnemyReached;
        }

        private void EndGame(GameOverType gameOverType)
        {
            _gameState.EndGame(gameOverType);
            Invoked?.Invoke(gameOverType);
        }

        private void Invoke()
        {
            if (_allEnemiesDied && _mainTargetDied)
                EndGame(GameOverType.Completed);
        }

        private void OnAllEnemyDied()
        {
            _allEnemiesDied = true;
            Invoke();
        }

        private void OnTargetDied()
        {
            _mainTargetDied = true;
            Invoke();
        }

        private void OnPlayerDied()
        {
            EndGame(GameOverType.PlayerDied);
        }

        private void OnEnemyReached()
        {
            EndGame(GameOverType.TargetHidden);
        }
    }
}