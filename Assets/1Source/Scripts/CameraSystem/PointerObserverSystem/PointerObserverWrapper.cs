using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.CameraSystem.PointerObserverSystem
{
    public class PointerObserverWrapper : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private PointerObserver _pointerObserver;

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerObserver.OnPointerDown(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _pointerObserver.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _pointerObserver.OnEndDrag(eventData);
        }
    }
}
