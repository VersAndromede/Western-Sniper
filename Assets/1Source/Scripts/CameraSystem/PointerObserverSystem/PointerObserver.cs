using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.GameConfigSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Scripts.CameraSystem.PointerObserverSystem
{
    public class PointerObserver : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private GameConfig _gameConfig;
        private CameraLookingAtPoint _cameraLookingAtHandler;
        private float _speed;
        private int _activePointerId = -1;

        public PointerObserverType Type { get; private set; } = PointerObserverType.ObserverScreen;

        public event Action Down;

        public event Action<PointerObserverType> DragEnded;

        public void ChangeType(PointerObserverType type)
        {
            Type = type;
            UpdateSpeed();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_activePointerId != -1)
                return;

            _activePointerId = eventData.pointerId;
            UpdateSpeed();
            Down?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerId != _activePointerId)
                return;

            _cameraLookingAtHandler.LookAtPointer(eventData, _speed);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerId != _activePointerId)
                return;

            _activePointerId = -1;
            DragEnded?.Invoke(Type);
        }

        [Inject]
        private void Construct(GameConfig gameConfig, CameraLookingAtPoint cameraLookingAtPoint)
        {
            _gameConfig = gameConfig;
            _cameraLookingAtHandler = cameraLookingAtPoint;
        }

        private void UpdateSpeed()
        {
            if (Type == PointerObserverType.ObserverScreen)
                _speed = _gameConfig.ObservationSpeed;
            else if (Type == PointerObserverType.AimButton)
                _speed = _gameConfig.AimingSpeed;
        }
    }
}
