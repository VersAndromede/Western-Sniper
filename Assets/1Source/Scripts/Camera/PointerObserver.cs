﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Scripts.CameraSystem
{
    public class PointerObserver : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        private GameConfig _gameConfig;
        private CameraObserver _cameraObserver;
        private float _speed;
        private int _activePointerId = -1;

        public PointerObserverType Type { get; private set; } = PointerObserverType.ObserverScreen;

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
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerId != _activePointerId)
                return;

            _cameraObserver.LookAtPointer(eventData, _speed);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerId != _activePointerId)
                return;

            _activePointerId = -1;
            DragEnded?.Invoke(Type);
        }

        [Inject]
        private void Construct(GameConfig gameConfig, CameraObserver cameraObserver)
        {
            _gameConfig = gameConfig;
            _cameraObserver = cameraObserver;
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
