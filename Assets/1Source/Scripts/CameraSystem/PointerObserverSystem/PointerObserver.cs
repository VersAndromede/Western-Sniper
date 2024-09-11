using Scripts.CameraSystem.CameraAimingSystem;
using Scripts.GameStateSystem;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Scripts.CameraSystem.PointerObserverSystem
{
    public class PointerObserver : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private ObservingCameraSpeedFactory _speedFactory;
        private CameraLookingAtPoint _cameraLookingAtHandler;
        private GameState _gameState;
        private float _speed;
        private int _activePointerId = -1;

        public event Action Down;

        public event Action<GameStateType> DragEnded;

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
            DragEnded?.Invoke(_gameState.Type);
        }

        [Inject]
        private void Construct(ObservingCameraSpeedFactory speedFactory, CameraLookingAtPoint cameraLookingAtPoint, GameState gameState)
        {
            _speedFactory = speedFactory;
            _cameraLookingAtHandler = cameraLookingAtPoint;
            _gameState = gameState;
        }

        private void UpdateSpeed()
        {
            _speed = _speedFactory.GetSpeed(_gameState.Type);
        }
    }
}
