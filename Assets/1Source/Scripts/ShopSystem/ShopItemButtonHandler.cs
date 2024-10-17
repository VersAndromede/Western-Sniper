using Modules.SavingsSystem;
using Scripts.CurrencySystem;
using System;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Scripts.ShopSystem
{
    public class ShopItemButtonHandler : MonoBehaviour
    {
        [SerializeField] private ShopItemButton[] _shopItemButtons;
        [SerializeField] private ShopButtonFaceBuy _shopButtonFaceBuy;

        private ShopItemsData _shopItemsData;
        private PlayerFace _playerFace;
        private PriceFace _priceFace;
        private Currency _currency;

        private void OnDestroy()
        {
            foreach (ShopItemButton shopItemButton in _shopItemButtons)
                shopItemButton.Clicked -= OnItemButtonClicked;

            _shopButtonFaceBuy.Clicked -= OnButtonFaceBuyClicked;
        }

        [Inject]
        private void Construct(ShopItemsData shopItemsData, PlayerFace playerFace, PriceFace priceFace, Currency currency)
        {
            _shopItemsData = shopItemsData;
            _playerFace = playerFace;
            _priceFace = priceFace;
            _currency = currency;

            if (_shopItemsData.ShopItems.Count == 0)
                for (int i = 0; i < _shopItemButtons.Length; i++)
                    _shopItemsData.ShopItems.Add(new ShopItem());

            _shopItemsData.UnlockItem(0);
            EnableItem(_shopItemButtons[_shopItemsData.SelectedFace]);

            for (int i = 0; i < _shopItemButtons.Length; i++)
            {
                if (_shopItemsData.ShopItems[i].Locked)
                    _shopItemButtons[i].Lock();

                _shopItemButtons[i].SetIcon(i);
                _shopItemButtons[i].Clicked += OnItemButtonClicked;
            }

            _shopButtonFaceBuy.Clicked += OnButtonFaceBuyClicked;
            _shopButtonFaceBuy.UpdatePriceView(_priceFace.Price);
        }

        private void EnableItem(ShopItemButton shopItemButton)
        {
            int index = Array.IndexOf(_shopItemButtons, shopItemButton);
            _shopItemsData.SetFace(index);
            _playerFace.Set(index);
            shopItemButton.Enable();
        }

        private void OnItemButtonClicked(ShopItemButton shopItemButton)
        {
            foreach (ShopItemButton button in _shopItemButtons)
                button.Disable();

            EnableItem(shopItemButton);
        }

        private void OnButtonFaceBuyClicked()
        {
            ShopItemButton shopItemButton = _shopItemButtons.FirstOrDefault(button => button.Unlocked == false);

            if (shopItemButton != null)
            {
                if (_currency.TryRemove((uint)_priceFace.Price))
                {
                    shopItemButton.Unlock();
                    int index = Array.IndexOf(_shopItemButtons, shopItemButton);
                    _shopItemsData.UnlockItem(index);
                    _priceFace.RaisePrice();
                    _shopButtonFaceBuy.UpdatePriceView(_priceFace.Price);
                }
            }
        }
    }
}