using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.CameraSystem
{
    public class SurveillanceCamera : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _focusPoint;
        [SerializeField] private float _speed;
        [SerializeField] private float _smoothTime;
        [SerializeField] private Vector2 _horizontalRestriction;
        [SerializeField] private Vector2 _verticalRestriction;
        [SerializeField] private AnimationCurve _animationCurve;

        private Quaternion _targetRotation;
        private Quaternion _currentRotation;
        private Vector3 _currentEulerAngles;

        private void Start()
        {
            _currentEulerAngles = _camera.transform.rotation.eulerAngles;
            _currentRotation = _camera.transform.rotation;
            _targetRotation = _currentRotation;
        }

        private void Update()
        {
            float lerpFactor = _animationCurve.Evaluate(_smoothTime * Time.deltaTime);
            _currentRotation = Quaternion.Slerp(_currentRotation, _targetRotation, lerpFactor);
            float distance = Vector3.Distance(_camera.transform.position, _focusPoint.position);
            Vector3 newPosition = _focusPoint.position - (_currentRotation * Vector3.forward * distance);

            _camera.transform.position = newPosition;
            _camera.transform.rotation = _currentRotation;
            _camera.transform.LookAt(_focusPoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _currentEulerAngles.y += eventData.delta.x * _speed;
            _currentEulerAngles.x -= eventData.delta.y * _speed;

            _currentEulerAngles.y = Mathf.Clamp(_currentEulerAngles.y, _horizontalRestriction.x, _horizontalRestriction.y);
            _currentEulerAngles.x = Mathf.Clamp(_currentEulerAngles.x, _verticalRestriction.x, _verticalRestriction.y);

            _targetRotation = Quaternion.Euler(_currentEulerAngles);
        }
    }
}
