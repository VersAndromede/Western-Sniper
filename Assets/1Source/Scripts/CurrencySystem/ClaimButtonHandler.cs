using Cysharp.Threading.Tasks;
using Scripts.LevelSystem;
using Scripts.UI;
using UnityEngine;
using VContainer;

namespace Scripts.CurrencySystem
{
    public class ClaimButtonHandler : MonoBehaviour
    {
        [SerializeField] private PressedButton _claimButton;
        [SerializeField] private CurrencyAccrualView _currencyAccrualView;

        private Currency _currency;
        private AmountCurrencyPerLevel _amountCurrencyPerLevel;
        private LevelLoader _levelLoader;

        [Inject]
        private void Construct(Currency currency, AmountCurrencyPerLevel amountCurrencyPerLevel, LevelLoader levelLoader)
        {
            _currency = currency;
            _amountCurrencyPerLevel = amountCurrencyPerLevel;
            _levelLoader = levelLoader;

            _claimButton.Click += OnClick;
        }

        private void OnDestroy()
        {
            _claimButton.Click -= OnClick;
        }

        private async UniTask AnimateCurrencyAccrual(int startCurrency)
        {
            await _currencyAccrualView.Animate(startCurrency, (int)_currency.Count);
            _levelLoader.LoadNext();
        }

        private void OnClick()
        {
            uint currencyCount = _amountCurrencyPerLevel.GetAmount();
            int startCurrency = (int)_currency.Count;

            _currency.Add(currencyCount);
            AnimateCurrencyAccrual(startCurrency);
        }
    }
}