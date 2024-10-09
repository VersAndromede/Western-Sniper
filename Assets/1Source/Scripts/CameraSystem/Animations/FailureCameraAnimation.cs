using UnityEngine;

namespace Scripts.CameraSystem.Animations
{
    public class FailureCameraAnimation : MonoBehaviour
    {
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private Transform _targetPoint;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _animationCurve;

        public void Animate()
        {
            _cameraMover.Move(_duration, _animationCurve, _targetPoint.position, _targetPoint.rotation);
        }
    }
}