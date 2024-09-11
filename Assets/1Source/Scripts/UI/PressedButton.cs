using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class PressedButton : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField] private Button _button;

        public event Action Down;

        public event Action Click;

        public void Lock()
        {
            _button.image.raycastTarget = false;
        }

        public void Unlock()
        {
            _button.image.raycastTarget = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Down?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke();
        }
    }
}