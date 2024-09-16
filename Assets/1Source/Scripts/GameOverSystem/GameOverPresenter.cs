using System;
using Scripts.CurrencySystem;
using VContainer;

namespace Scripts.GameOverSystem
{
    public class GameOverPresenter : IDisposable
    {
        private readonly GameOver _gameOver;
        private readonly VictoryScreenView _victoryView;
        private readonly FailedScreenView _failedView;
        private readonly AmountCurrencyPerLevel _amountCurrencyPerLevel;
        private readonly AmountCurrencyPerLevelView _amountCurrencyPerLevelView;

        [Inject]
        public GameOverPresenter(GameOver gameOver, VictoryScreenView victoryView, FailedScreenView failedView, AmountCurrencyPerLevel amountCurrencyPerLevel, AmountCurrencyPerLevelView amountCurrencyPerLevelView)
        {
            _gameOver = gameOver;
            _victoryView = victoryView;
            _failedView = failedView;
            _amountCurrencyPerLevel = amountCurrencyPerLevel;
            _amountCurrencyPerLevelView = amountCurrencyPerLevelView;

            _gameOver.Invoked += OnInvoked;
        }

        public void Dispose()
        {
            _gameOver.Invoked -= OnInvoked;
        }

        private void EnableVictory()
        {
            _amountCurrencyPerLevelView.UpdateView(_amountCurrencyPerLevel.LevelReward, _amountCurrencyPerLevel.HeadshotBonus);
            _victoryView.Enable();
        }

        private void EnableFaile()
        {
            _failedView.Enable();
        }

        private void OnInvoked(GameOverType gameOverType)
        {
            switch (gameOverType)
            {
                case GameOverType.Completed:
                    EnableVictory();
                    break;
                case GameOverType.Failed:
                    EnableFaile();
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}