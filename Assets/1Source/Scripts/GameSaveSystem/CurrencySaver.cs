using System;
using Scripts.CurrencySystem;
using VContainer;

namespace Modules.SavingsSystem
{
    public class CurrencySaver : IDisposable
    {
        private readonly Currency _currency;
        private readonly CurrencyAccrualView _currencyAccrualView;
        private readonly SaveSystem _saveSystem;

        [Inject]
        public CurrencySaver(Currency currency, CurrencyAccrualView currencyAccrualView, SaveSystem saveSystem)
        {
            _currency = currency;
            _saveSystem = saveSystem;
            _currencyAccrualView = currencyAccrualView;

            _currency.Add(_saveSystem.Load().Currency);
            _currencyAccrualView.UpdateView((int)currency.Count);

            _currency.Changed += OnChanged;
        }

        public void Dispose()
        {
            _currency.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _currencyAccrualView.UpdateView((int)_currency.Count);

            _saveSystem.Save(data =>
            {
                data.Currency = _currency.Count;
            });
        }
    }
}