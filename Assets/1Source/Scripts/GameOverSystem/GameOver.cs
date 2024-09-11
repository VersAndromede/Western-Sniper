using System;
using EnemyCounterSystem;
using Scripts.EnemySystem;
using Scripts.GameStateSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using VContainer;

namespace Scripts.GameOverSystem
{
    public class GameOver
    {
        private readonly EnemyCounter _enemyCounter;
        private readonly MainEnemy _mainTarget;
        private readonly GameState _gameState;

        private bool _allEnemiesDied;
        private bool _mainTargetDied;

        public event Action Invoked;

        [Inject]
        public GameOver(EnemyCounter enemyCounter, MainEnemy mainEnemy, GameState gameState)
        {
            _enemyCounter = enemyCounter;
            _mainTarget = mainEnemy;
            _gameState = gameState;

            _enemyCounter.AllDied += OnAllDied;
            _mainTarget.Died += OnDied;
        }

        public void Unsubscribe()
        {
            _enemyCounter.AllDied -= OnAllDied;
            _mainTarget.Died -= OnDied;
        }

        private void OnAllDied()
        {
            _allEnemiesDied = true;
            Invoke();
        }

        private void OnDied()
        {
            _mainTargetDied = true;
            Invoke();
        }

        private void Invoke()
        {
            if (_allEnemiesDied && _mainTargetDied)
            {
                _gameState.ChangeType(GameStateType.Over);
                Invoked?.Invoke();
            }
        }
    }
}