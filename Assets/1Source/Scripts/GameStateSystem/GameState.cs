using Scripts.CameraSystem.PointerObserverSystem;

namespace Scripts.GameStateSystem
{
    public class GameState
    {
        public GameStateType Type { get; private set; } = GameStateType.Observation;

        public void ChangeType(GameStateType type)
        {
            Type = type;
        }
    }
}