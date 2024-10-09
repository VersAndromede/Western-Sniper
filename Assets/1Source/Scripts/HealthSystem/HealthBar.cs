using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.HealthSystem
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] protected Slider Slider;
        [SerializeField] protected float ChangeDuration;
        [SerializeField] protected AnimationCurve AnimationCurve;

        private Tween _doValueTween;

        public void UpdateUI(int health, int maxHealth)
        {
            if (health == 0)
                HandleZeroHealth();

            HandleBeforeUpdateUI();

            _doValueTween?.Kill();
            float sliderValue = (float)health / maxHealth;
            _doValueTween = Slider.DOValue(sliderValue, ChangeDuration).SetEase(AnimationCurve);

            HandleAfterUpdateUI();
        }

        protected virtual void HandleZeroHealth() { }

        protected virtual void HandleBeforeUpdateUI() { }

        protected virtual void HandleAfterUpdateUI() { }
    }
}