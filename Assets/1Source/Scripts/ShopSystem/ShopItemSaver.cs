using Modules.SavingsSystem;
using System;

namespace Scripts.ShopSystem
{
    public class ShopItemSaver : IDisposable
    {
        private readonly SaveSystem _saveSystem;
        private readonly ShopItemsData _itemsData;

        public ShopItemSaver(ShopItemsData itemsData, SaveSystem saveSystem)
        {
            _itemsData = itemsData;

            _itemsData.Changed += OnChanged;
            _saveSystem = saveSystem;
        }

        public void Dispose()
        {
            _itemsData.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _saveSystem.Save(data =>
            {
                data.ShopItemsData = _itemsData;
            });
        }
    }
}