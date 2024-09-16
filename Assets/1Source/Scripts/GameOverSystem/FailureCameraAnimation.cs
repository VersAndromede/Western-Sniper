using UnityEngine;
using Scripts.Utilities;
using Scripts.EnemySystem;
using Scripts.CameraSystem.CameraAimingSystem;

namespace Scripts.GameOverSystem
{
    public class FailureCameraAnimation : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private MainEnemyTargetTrigger _targetPoint;
        [SerializeField] private CameraLookingAtPoint _cameraLookingAtPoint;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _animationCurve;

        public void Animate()
        {
            Vector3 startPosition = Camera.main.transform.position;
            Quaternion startRotation = Camera.main.transform.rotation;

            _cameraLookingAtPoint.Disable();

            ValueEffectorUtility.Animate(
                _duration,
                _animationCurve,
                destroyCancellationToken,
                lerpFactor => Vector3.LerpUnclamped(startPosition, _targetPoint.PositionForCamera, lerpFactor),
                newPosition => _camera.transform.position = newPosition);

            ValueEffectorUtility.Animate(
                _duration,
                _animationCurve,
                destroyCancellationToken,
                lerpFactor => Quaternion.SlerpUnclamped(startRotation, _targetPoint.RotationForCamera, lerpFactor),
                newRotation => _camera.transform.rotation = newRotation);
        }
    }
}