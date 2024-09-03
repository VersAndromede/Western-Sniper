using System;
using System.Collections;
using UnityEngine;

namespace Scripts.CameraSystem
{
    public class CameraAiming : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _focusPoint;
        [SerializeField] private float _targetFieldOfView;
        [SerializeField] private float _targetMovingForward;
        [SerializeField] private float _travelTime;
        [SerializeField] private AnimationCurve _animationCurve;

        private float _defaultFieldOfView;
        private bool _isAimingWork;

        public event Action AimingInitiated;

        public event Action AimingCompleted;

        public event Action CameraReturned;

        private void Start()
        {
            _defaultFieldOfView = _camera.fieldOfView;
        }

        public IEnumerator StartAim()
        {
            if (_isAimingWork)
                yield break;

            _isAimingWork = true;
            AimingInitiated?.Invoke();
            StartCoroutine(Aim(_defaultFieldOfView, _targetFieldOfView));
            yield return StartCoroutine(Move(_camera.transform.forward));
            AimingCompleted?.Invoke();
        }

        public IEnumerator EndAim()
        {
            StartCoroutine(Aim(_targetFieldOfView, _defaultFieldOfView));
            yield return StartCoroutine(Move(-_camera.transform.forward));
            _isAimingWork = false;
            CameraReturned?.Invoke();
        }

        private IEnumerator Aim(float start, float end)
        {
            float elapsedTime = 0;

            while (elapsedTime < _travelTime)
            {
                float lerpFactor = _animationCurve.Evaluate(elapsedTime / _travelTime);
                float newFieldOfView = Mathf.Lerp(start, end, lerpFactor);
                _camera.fieldOfView = newFieldOfView;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _camera.fieldOfView = end;
        }

        private IEnumerator Move(Vector3 direction)
        {
            Vector3 currentPozition = _camera.transform.position;
            Vector3 target = currentPozition + direction.normalized * _targetMovingForward;
            float elapsedTime = 0;

            while (elapsedTime < _travelTime)
            {
                float lerpFactor = _animationCurve.Evaluate(elapsedTime / _travelTime);
                Vector3 newPozition = Vector3.LerpUnclamped(currentPozition, target, lerpFactor);
                _camera.transform.position = newPozition;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _camera.transform.position = target;
        }
    }
}