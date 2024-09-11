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

        [Inject]
        private void Construct(Currency currency, AmountCurrencyPerLevel amountCurrencyPerLevel)
        {
            _currency = currency;
            _amountCurrencyPerLevel = amountCurrencyPerLevel;

            _claimButton.Click += OnClick;
        }

        private void OnDestroy()
        {
            _claimButton.Click -= OnClick;
        }

        private void OnClick()
        {
            uint currencyCount = _amountCurrencyPerLevel.GetAmount();
            int startCurrency = _currency.Count;

            _currency.Add(currencyCount);
            _currencyAccrualView.Animate(startCurrency, _currency.Count);
        }
    }
}