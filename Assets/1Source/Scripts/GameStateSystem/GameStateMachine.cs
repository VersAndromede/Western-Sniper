using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.CameraSystem.PointerObserverSystem;
using Scripts.UI;
using UnityEngine;

namespace Scripts.GameStateSystem
{
    public class GameplayUIContainer : MonoBehaviour
    {
        [field: SerializeField] public AnimatedButton ExitAimingButton { get; private set; }

        [field: SerializeField] public AnimatedButton AimButton { get; private set; }

        [field: SerializeField] public PointerObserver ScreenObserver { get; private set; }

        [field: SerializeField] public GameObject Crosshairs { get; private set; }
    }

    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private AimingExitService _aimingExitService;
        [SerializeField] private GameplayUIContainer _gameplayUIContainer;

        public void EnableGameplayScreen()
        {
            _aimingExitService.HideButton(_gameplayUIContainer.ExitAimingButton);
            _aimingExitService.Activate(gameObject);
            _aimingExitService.Exit();
        }
    }
}