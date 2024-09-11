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
}