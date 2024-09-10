using System;
using VContainer;

namespace Scripts.GameOverSystem
{
    public class GameOverPresenter : IDisposable
    {
        private readonly GameOver _gameOver;
        private readonly GameOverView _view;

        [Inject]
        public GameOverPresenter(GameOver gameOver, GameOverView view)
        {
            _gameOver = gameOver;
            _view = view;

            _gameOver.Invoked += OnInvoked;
        }

        public void Dispose()
        {
            _gameOver.Invoked -= OnInvoked;
        }

        public void OnInvoked()
        {
            _view.Enable();
        }
    }
}