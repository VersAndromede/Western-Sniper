using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.CameraSystem.PointerObserverSystem
{
    public class PointerObserverWrapper : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
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

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerObserver.OnPointerUp(eventData);
        }
    }
}
