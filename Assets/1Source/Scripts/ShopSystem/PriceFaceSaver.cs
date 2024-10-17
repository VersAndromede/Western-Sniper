using Modules.SavingsSystem;
using System;

namespace Scripts.ShopSystem
{
    public class PriceFaceSaver : IDisposable
    {
        private readonly PriceFace _priceFace;
        private readonly SaveSystem _saveSystem;

        public PriceFaceSaver(PriceFace priceFace, SaveSystem saveSystem)
        {
            _priceFace = priceFace;
            _saveSystem = saveSystem;

            _priceFace.Changed += OnChanged;
        }

        public void Dispose()
        {
            _priceFace.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _saveSystem.Save(data =>
            {
                data.FacePriceIndex = _priceFace.CurrentPriceIndex;
            });
        }
    }
}