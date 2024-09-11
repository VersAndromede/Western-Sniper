using Scripts.CurrencySystem;
using System;
using VContainer;

namespace Scripts.GameOverSystem
{
    public class GameOverPresenter : IDisposable
    {
        private readonly GameOver _gameOver;
        private readonly GameOverView _view;
        private readonly AmountCurrencyPerLevel _amountCurrencyPerLevel;
        private readonly AmountCurrencyPerLevelView _amountCurrencyPerLevelView;

        [Inject]
        public GameOverPresenter(GameOver gameOver, GameOverView view, AmountCurrencyPerLevel amountCurrencyPerLevel, AmountCurrencyPerLevelView amountCurrencyPerLevelView)
        {
            _gameOver = gameOver;
            _view = view;
            _amountCurrencyPerLevel = amountCurrencyPerLevel;
            _amountCurrencyPerLevelView = amountCurrencyPerLevelView;

            _gameOver.Invoked += OnInvoked;
        }

        public void Dispose()
        {
            _gameOver.Invoked -= OnInvoked;
        }

        public void OnInvoked()
        {
            _amountCurrencyPerLevelView.UpdateView(_amountCurrencyPerLevel.LevelReward, _amountCurrencyPerLevel.HeadshotBonus);
            _view.Enable();
        }
    }
}