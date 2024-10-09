using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Scripts.CurrencySystem
{
    public class CurrencyAccrualView : MonoBehaviour
    {
        [SerializeField] private CurrencyIcon _prefab;
        [SerializeField] private TextMeshProUGUI _currencyText;
        [SerializeField] private RectTransform _container;
        [SerializeField] private RectTransform _startPoint;
        [SerializeField] private RectTransform _endPoint;
        [SerializeField] private float _accrualDuration;
        [SerializeField] private uint _numberCreditedElements;
        [SerializeField] private float _delayBetweenCreation;
        [SerializeField] private AnimationCurve _animationCurve;

        public event Action Collected;

        public void UpdateView(int currencyValue)
        {
            _currencyText.text = currencyValue.ToString();
        }

        public async UniTask Animate(int startCurrency, int endCurrency)
        {
            float textUpdateDuration = _accrualDuration + _delayBetweenCreation * _numberCreditedElements;

            DOVirtual.Int(startCurrency, endCurrency, textUpdateDuration, currencyValue =>
            {
                UpdateView(currencyValue);
            }).SetEase(_animationCurve);

            for (int i = 0; i < _numberCreditedElements; i++)
            {
                CurrencyIcon icon = Instantiate(_prefab, _container);
                icon.transform.position = _startPoint.position;
                icon.transform.DOMove(_endPoint.position, _accrualDuration).SetEase(_animationCurve);
                await UniTask.WaitForSeconds(_delayBetweenCreation, cancellationToken: destroyCancellationToken);
            }

            Collected?.Invoke();
        }
    }
}