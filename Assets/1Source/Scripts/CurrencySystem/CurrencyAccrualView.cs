using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting.FullSerializer;
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

        public async UniTask Animate(int startCurrency, int endCurrency)
        {
            float textUpdateDuration = _accrualDuration + _delayBetweenCreation * _numberCreditedElements;

            DOVirtual.Int(startCurrency, endCurrency, textUpdateDuration, currencyValue =>
            {
                _currencyText.text = currencyValue.ToString();
            }).SetEase(_animationCurve);

            for (int i = 0; i < _numberCreditedElements; i++)
            {
                CurrencyIcon icon = Instantiate(_prefab, _container);
                icon.transform.position = _startPoint.position;
                icon.transform.DOMove(_endPoint.position, _accrualDuration).SetEase(_animationCurve);
                await UniTask.WaitForSeconds(_delayBetweenCreation, cancellationToken: destroyCancellationToken);
            }
        }
    }
}