using UnityEngine;
using Scripts.Utilities;
using Scripts.CameraSystem.CameraAimingSystem;

namespace Scripts.CameraSystem.Animations
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraLookingAtPoint _cameraLookingAtPoint;

        public void Move(float duration, AnimationCurve animationCurve, Vector3 targetPosition, Quaternion targetRotation)
        {
            Vector3 startPosition = Camera.main.transform.position;
            Quaternion startRotation = Camera.main.transform.rotation;
            _cameraLookingAtPoint.Disable();

            ValueEffectorUtility.Animate(
                duration,
                animationCurve,
                destroyCancellationToken,
                lerpFactor => Vector3.LerpUnclamped(startPosition, targetPosition, lerpFactor),
                newPosition => _camera.transform.position = newPosition);

            ValueEffectorUtility.Animate(
                duration,
                animationCurve,
                destroyCancellationToken,
                lerpFactor => Quaternion.SlerpUnclamped(startRotation, targetRotation, lerpFactor),
                newRotation => _camera.transform.rotation = newRotation);
        }
    }
}