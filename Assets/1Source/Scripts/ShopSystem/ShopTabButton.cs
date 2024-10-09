using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.ShopSystem
{
    public class ShopTabButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Transform _enabledParent;
        [SerializeField] private Transform _disabledParent;
        [SerializeField] private Image _border;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _background;
        [SerializeField] private Color _disabledColor;
        [SerializeField] private Color _backgroundDisabledColor;
        [SerializeField] private Color _backgroundEnabledColor;

        public event Action<ShopTabButton> Clicked;

        public void Enable()
        {
            _border.color = Color.white;
            _icon.color = Color.white;
            _background.color = _backgroundEnabledColor;
            transform.SetParent(_enabledParent);
        }

        public void Disable()
        {
            _border.color = _disabledColor;
            _icon.color = _disabledColor;
            _background.color = _backgroundDisabledColor;
            transform.SetParent(_disabledParent);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
    }
}