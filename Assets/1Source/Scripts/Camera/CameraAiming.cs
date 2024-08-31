using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        private Vector3 _startPozition;
        private bool _isAimingWork;

        public event Action Aimed;

        private void Start()
        {
            _defaultFieldOfView = _camera.fieldOfView;
        }

        public void StartAim()
        {
            if (_isAimingWork)
                return;

            _isAimingWork = true;
            Aimed?.Invoke();
            StartCoroutine(Aim(_defaultFieldOfView, _targetFieldOfView));
            StartCoroutine(MoveForward());
        }

        public void EndAim()
        {
            StartCoroutine(Aim(_targetFieldOfView, _defaultFieldOfView));
            StartCoroutine(MoveBackward());
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

        private IEnumerator MoveForward()
        {
            _startPozition = _camera.transform.position;
            Vector3 direction = (_focusPoint.position - _camera.transform.position).normalized;
            Vector3 target = _startPozition + direction * _targetMovingForward;
            float elapsedTime = 0;

            while (elapsedTime < _travelTime)
            {
                float lerpFactor = _animationCurve.Evaluate(elapsedTime / _travelTime);
                Vector3 newPozition = Vector3.Lerp(_startPozition, target, lerpFactor);
                _camera.transform.position = newPozition;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _camera.transform.position = target;
        }

        private IEnumerator MoveBackward()
        {
            Vector3 currentPozition = _camera.transform.position;
            Vector3 direction = (_camera.transform.position - _focusPoint.position).normalized;
            Vector3 target = currentPozition + direction * _targetMovingForward;
            float elapsedTime = 0;

            while (elapsedTime < _travelTime)
            {
                float lerpFactor = _animationCurve.Evaluate(elapsedTime / _travelTime);
                Vector3 newPozition = Vector3.Lerp(currentPozition, target, lerpFactor);
                _camera.transform.position = newPozition;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _camera.transform.position = target;
            _isAimingWork = false;
        }
    }
}