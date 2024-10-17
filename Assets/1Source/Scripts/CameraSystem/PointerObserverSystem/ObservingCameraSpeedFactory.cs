using Scripts.GameConfigSystem;
using UnityEngine;
using VContainer;

namespace Scripts.CameraSystem.PointerObserverSystem
{
    public class ObservingCameraSpeedFactory
    {
        private readonly GameConfig _gameConfig;

        [Inject]
        public ObservingCameraSpeedFactory(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public float GetSpeed(GameStateType gameStateType)
        {
            switch (gameStateType)
            {
                case GameStateType.Observation:
                    return _gameConfig.ObservationSpeed;
                case GameStateType.Aiming:
                    return _gameConfig.AimingSpeed;
                default:
                    return _gameConfig.ObservationSpeed;
            }
        }
    }
}
