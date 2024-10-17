using Scripts.GameConfigSystem;
using System;

namespace Scripts.ShopSystem
{
    public class PriceFace
    {
        private readonly GameConfig _gameConfig;
        
        public PriceFace(GameConfig gameConfig, int currentPriceIndex)
        {
            _gameConfig = gameConfig;
            CurrentPriceIndex = currentPriceIndex;
        }

        public int CurrentPriceIndex { get; private set; }

        public int Price => _gameConfig.FacePrices[CurrentPriceIndex];

        public event Action Changed;

        public void RaisePrice()
        {
            CurrentPriceIndex++;
            Changed?.Invoke();
        }
    }
}