using Cysharp.Threading.Tasks;
using Scripts.Utilities;
using System;
using System.Threading;
using UnityEngine;

namespace Scripts.CameraSystem.CameraAimingSystem
{
    public class CameraAiming : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _targetFieldOfView;
        [SerializeField] private float _targetMovingForward;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _animationCurve;

        private float _defaultFieldOfView;
        private bool _isAimingInProgress;

        public event Action AimingInitiated;

        public event Action AimingCompleted;

        public event Action CameraReturned;

        private void Start()
        {
            _defaultFieldOfView = _camera.fieldOfView;
        }

        public async UniTask StartAim(CancellationToken cancellationToken)
        {
            if (_isAimingInProgress)
                return;

            _isAimingInProgress = true;
            AimingInitiated?.Invoke();

            SetFieldOfView(_defaultFieldOfView, _targetFieldOfView, cancellationToken);
            await Move(_camera.transform.forward, cancellationToken);

            AimingCompleted?.Invoke();
        }

        public async UniTask EndAim(CancellationToken cancellationToken)
        {
            SetFieldOfView(_targetFieldOfView, _defaultFieldOfView, cancellationToken);
            await Move(-_camera.transform.forward, cancellationToken);

            _isAimingInProgress = false;
            CameraReturned?.Invoke();
        }

        private async UniTask SetFieldOfView(float startFieldOfView, float endFieldOfView, CancellationToken cancellationToken)
        {
            await ValueEffectorUtility.Animate(
                _duration,
                _animationCurve,
                cancellationToken,
                lerpFactor => Mathf.LerpUnclamped(startFieldOfView, endFieldOfView, lerpFactor),
                newFieldOfView => _camera.fieldOfView = newFieldOfView,
                () => _camera.fieldOfView = endFieldOfView);
        }

        private async UniTask Move(Vector3 direction, CancellationToken cancellationToken)
        {
            Vector3 currentPosition = _camera.transform.position;
            Vector3 target = currentPosition + direction.normalized * _targetMovingForward;

            await ValueEffectorUtility.Animate(
                _duration,
                _animationCurve,
                cancellationToken,
                lerpFactor => Vector3.LerpUnclamped(currentPosition, target, lerpFactor),
                newPosition => _camera.transform.position = newPosition,
                () => _camera.transform.position = target);
        }
    }
}