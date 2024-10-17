using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.ShopSystem
{
    public class ShopButtonFaceBuy : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _text;

        public event Action Clicked;

        public void UpdatePriceView(int price)
        {
            _text.text = price.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }
    }
}