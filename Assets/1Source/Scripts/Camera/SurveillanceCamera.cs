using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.CameraSystem
{
    public class SurveillanceCamera : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _focusPoint;
        [SerializeField] private float _speed;
        [SerializeField] private float _smoothTime;
        [SerializeField] private Vector2 _horizontalRestriction;
        [SerializeField] private Vector2 _verticalRestriction;
        [SerializeField] private AnimationCurve _animationCurve;

        private Quaternion _targetRotation;
        private Vector3 _currentEulerAngles;
        private bool _isDragging;

        private void Start()
        {
            _currentEulerAngles = _camera.transform.rotation.eulerAngles;
            _targetRotation = _camera.transform.rotation;
            RotateCamera();
        }

        private void Update()
        {
            if (_isDragging)
            {
                float lerpFactor = _animationCurve.Evaluate(_smoothTime * Time.deltaTime);
                _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, _targetRotation, lerpFactor);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _currentEulerAngles.y += eventData.delta.x * _speed;
            _currentEulerAngles.x -= eventData.delta.y * _speed;
            RotateCamera();
        }

        private void RotateCamera()
        {
            _currentEulerAngles.y = Mathf.Clamp(_currentEulerAngles.y, _horizontalRestriction.x, _horizontalRestriction.y);
            _currentEulerAngles.x = Mathf.Clamp(_currentEulerAngles.x, _verticalRestriction.x, _verticalRestriction.y);

            Quaternion newRotation = Quaternion.Euler(_currentEulerAngles);
            float distance = Vector3.Distance(_camera.transform.position, _focusPoint.position);
            Vector3 newPosition = _focusPoint.position - (newRotation * Vector3.forward * distance);

            _camera.transform.position = newPosition;
            _camera.transform.LookAt(_focusPoint);

            _targetRotation = _camera.transform.rotation;
        }
    }
}
