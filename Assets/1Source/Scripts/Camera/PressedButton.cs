using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.CameraSystem
{
    public class PressedButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Button _button;

        public event Action Down;

        public event Action Clicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void Lock()
        {
            _button.enabled = false;
        }

        public void Unlock()
        {
            _button.enabled = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Down.Invoke();
        }

        private void OnClick()
        {
            Clicked?.Invoke();
        }
    }
}