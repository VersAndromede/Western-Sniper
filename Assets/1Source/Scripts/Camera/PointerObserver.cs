using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

namespace Scripts.CameraSystem
{
    public class PointerObserver : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [field: SerializeField] public PointerObserverType Type { get; private set; }

        private GameConfig _gameConfig;
        private CameraObserver _cameraObserver;
        private float _speed;

        public event Action DragStarted;
        public event Action<PointerObserverType> DragEnded;

        public void ChangeType(PointerObserverType type)
        {
            Type = type;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Type == PointerObserverType.ObserverScreen)
                _speed = _gameConfig.ObservationSpeed;
            else if (Type == PointerObserverType.AimButton)
                _speed = _gameConfig.AimingSpeed;

            DragStarted?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _cameraObserver.LookAtPointer(eventData, _speed);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragEnded?.Invoke(Type);
        }

        [Inject]
        private void Construct(GameConfig gameConfig, CameraObserver cameraObserver)
        {
            _gameConfig = gameConfig;
            _cameraObserver = cameraObserver;
        }
    }
}
