using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.HealthSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _changeDuration;
        [SerializeField] private float _activeTime;
        [SerializeField] private AnimationCurve _animationCurve;

        private Tween _doValueTween;
        private Coroutine _disableSliderJob;
        private WaitForSeconds _wait;

        private void Start()
        {
            _wait = new WaitForSeconds(_activeTime);
        }

        public void UpdateUI(int health, int maxHealth)
        {
            if (health == 0)
            {
                _slider.gameObject.SetActive(false);
                return;
            }

            if (_disableSliderJob != null)
                StopCoroutine(_disableSliderJob);

            _doValueTween?.Kill();
            _slider.gameObject.SetActive(true);

            float sliderValue = (float)health / maxHealth;
            _doValueTween = _slider.DOValue(sliderValue, _changeDuration).SetEase(_animationCurve);
            _disableSliderJob = StartCoroutine(DisableSlider());
        }

        private IEnumerator DisableSlider()
        {
            yield return _wait;
            _slider.gameObject.SetActive(false);
        }
    }
}