using UnityEngine;

namespace Scripts.CameraSystem
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float ObservationSpeed { get; private set; }

        [field: SerializeField] public float AimingSpeed { get; private set; }
    }
}