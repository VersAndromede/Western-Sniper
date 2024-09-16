using System;
using EnemyCounterSystem;
using Scripts.EnemySystem;
using Scripts.GameStateSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using VContainer;
using UnityEngine;

namespace Scripts.GameOverSystem
{
    public class GameOver
    {
        private readonly EnemyCounter _enemyCounter;
        private readonly MainEnemy _mainTarget;
        private readonly MainEnemyTargetTrigger _mainEnemyTargetTrigger;
        private readonly GameState _gameState;

        private bool _allEnemiesDied;
        private bool _mainTargetDied;

        public event Action<GameOverType> Invoked;

        [Inject]
        public GameOver(EnemyCounter enemyCounter, MainEnemy mainEnemy, MainEnemyTargetTrigger mainEnemyTargetTrigger, GameState gameState)
        {
            _enemyCounter = enemyCounter;
            _mainTarget = mainEnemy;
            _mainEnemyTargetTrigger = mainEnemyTargetTrigger;
            _gameState = gameState;

            _enemyCounter.AllDied += OnAllDied;
            _mainTarget.Died += OnDied;
            _mainEnemyTargetTrigger.EnemyReached += OnEnemyReached;
        }

        public void Unsubscribe()
        {
            _enemyCounter.AllDied -= OnAllDied;
            _mainTarget.Died -= OnDied;
            _mainEnemyTargetTrigger.EnemyReached -= OnEnemyReached;
        }

        private void Invoke()
        {
            if (_allEnemiesDied && _mainTargetDied)
            {
                _gameState.EndGame();
                Invoked?.Invoke(GameOverType.Completed);
            }
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

        private void OnEnemyReached()
        {
            _gameState.EndGame();
            Invoked?.Invoke(GameOverType.Failed);
        }
    }
}