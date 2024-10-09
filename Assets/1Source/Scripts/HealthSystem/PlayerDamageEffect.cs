using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.HealthSystem
{
    public class PlayerDamageEffect : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _duration;
        [SerializeField] private AnimationCurve _animationCurve;

        private Tween _tween;

        public void Play()
        {
            const float FullAlpha = 1;

            _tween?.Kill();
            _tween = _image.DOFade(FullAlpha, _duration).SetEase(_animationCurve);
        }
    }
}