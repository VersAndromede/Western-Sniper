using Scripts.GameStateSystem;
using Scripts.UI;
using UnityEngine;
using VContainer;

namespace Scripts.CameraSystem.PointerObserverSystem
{
    public class PointerObserverHandler : MonoBehaviour
    {
        [SerializeField] private PressedButton _aimButton;
        [SerializeField] private PressedButton _exitAimingButton;
        
        private GameState _gameState;

        private void OnDestroy()
        {
            _aimButton.Down -= OnAimButtonDown;
            _exitAimingButton.Down -= OnExitButtonDown;
        }

        [Inject]
        private void Construct(GameState gameState)
        {
            _gameState = gameState;

            _aimButton.Down += OnAimButtonDown;
            _exitAimingButton.Down += OnExitButtonDown;
        }

        private void OnAimButtonDown()
        {
            _gameState.ChangeType(GameStateType.Aiming);
        }

        private void OnExitButtonDown()
        {
            _gameState.ChangeType(GameStateType.Observation);
        }
    }
}
