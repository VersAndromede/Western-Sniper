using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.CameraSystem.CameraAimingSystem
{
    public class CameraLookingAtPoint : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _focusPoint;
        [SerializeField] private float _smoothTime;
        [SerializeField] private Vector2 _horizontalRestriction;
        [SerializeField] private Vector2 _verticalRestriction;
        [SerializeField] private AnimationCurve _animationCurve;

        private Quaternion _currentRotation;
        private Quaternion _targetRotation;
        private Vector3 _currentEulerAngles;

        private void Start()
        {
            _currentEulerAngles = _camera.rotation.eulerAngles;
            _currentRotation = _camera.rotation;
            _targetRotation = _currentRotation;
        }

        private void LateUpdate()
        {
            float lerpFactor = _animationCurve.Evaluate(_smoothTime * Time.deltaTime);
            _currentRotation = Quaternion.Slerp(_currentRotation, _targetRotation, lerpFactor);
            float distance = Vector3.Distance(_camera.position, _focusPoint.position);
            Vector3 newPosition = _focusPoint.position - (_currentRotation * Vector3.forward * distance);
            _camera.SetPositionAndRotation(newPosition, _currentRotation);
        }

        public void LookAtPointer(PointerEventData eventData, float speed)
        {
            _currentEulerAngles.y += eventData.delta.x * speed;
            _currentEulerAngles.x -= eventData.delta.y * speed;

            _currentEulerAngles.y = Mathf.Clamp(_currentEulerAngles.y, _horizontalRestriction.x, _horizontalRestriction.y);
            _currentEulerAngles.x = Mathf.Clamp(_currentEulerAngles.x, _verticalRestriction.x, _verticalRestriction.y);

            _targetRotation = Quaternion.Euler(_currentEulerAngles);
        }
    }
}
