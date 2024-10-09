using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.GameOverSystem;
using System;

namespace Scripts.GameStateSystem
{
    public class GameState
    {
        public GameStateType Type { get; private set; } = GameStateType.Observation;

        public bool IsGameOver { get; private set; }

        public GameOverType GameOverType { get; private set; }

        public event Action Changed;

        public void ChangeType(GameStateType type)
        {
            Type = type;
            Changed?.Invoke();
        }

        public void EndGame(GameOverType gameOverType)
        {
            IsGameOver = true;
            GameOverType = gameOverType;
            Changed?.Invoke();
        }
    }
}