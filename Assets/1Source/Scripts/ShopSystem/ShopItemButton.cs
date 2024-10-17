using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.ShopSystem
{
    public class ShopItemButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject _closed;
        [SerializeField] private GameObject _border;
        [SerializeField] private Image _icon;
        [SerializeField] private Sprite[] _icons;

        public event Action<ShopItemButton> Clicked;

        public bool Unlocked { get; private set; } = true;

        public void SetIcon(int index)
        {
            _icon.sprite = _icons[index];
        }

        public void Unlock()
        {
            Debug.Log(transform.parent.gameObject.name);
            _closed.SetActive(false);
            Unlocked = true;
        }

        public void Lock()
        {
            _closed.SetActive(true);
            Unlocked = false;
        }

        public void Enable()
        {
            _border.SetActive(true);
        }

        public void Disable()
        {
            _border.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Unlocked)
                Clicked?.Invoke(this);
        }
    }
}