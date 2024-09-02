using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class PressedButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Button _button;

        public event Action Down;

        public void Lock()
        {
            _button.interactable = false;
        }

        public void Unlock()
        {
            _button.interactable = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Down?.Invoke();
        }
    }
}