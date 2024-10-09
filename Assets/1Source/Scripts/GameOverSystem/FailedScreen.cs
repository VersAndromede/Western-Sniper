using System;
using DG.Tweening;
using UnityEngine;
using TMPro;

namespace Scripts.GameOverSystem
{
    public class FailedScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _container;
        [SerializeField] private TextMeshProUGUI _textReason;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private string _targetHiddenReason;
        [SerializeField] private string _playerDeadReason;

        public void Show(GameOverType gameOverType)
        {
            const int TargetFade = 1;

            _container.gameObject.SetActive(true);
            _container.DOFade(TargetFade, _fadeDuration);
            _textReason.text = GetReason(gameOverType);
        }

        private string GetReason(GameOverType reason)
        {
            switch (reason)
            {
                case GameOverType.TargetHidden:
                    return _targetHiddenReason;
                case GameOverType.PlayerDied:
                    return _playerDeadReason;
                default:
                    throw new ArgumentException();
            }
        }
    }
}