﻿using UnityEngine;

namespace Scripts.ShopSystem
{
    public class ShopTabButtonHandler : MonoBehaviour 
    {
        [SerializeField] private ShopTabButton[] _shopTabButtons;
        [SerializeField] private ShopTabButton _enabledButtonOnStart;

        private void OnDestroy()
        {
            foreach (ShopTabButton shopTabButton in _shopTabButtons)
                shopTabButton.Clicked -= OnClicked;
        }

        private void Start()
        {
            _enabledButtonOnStart.Enable();

            foreach (ShopTabButton shopTabButton in _shopTabButtons)
                shopTabButton.Clicked += OnClicked;
        }

        private void OnClicked(ShopTabButton shopTabButton)
        {
            foreach (ShopTabButton button in _shopTabButtons)
                button.Disable();

            shopTabButton.Enable();
        }
    }
}