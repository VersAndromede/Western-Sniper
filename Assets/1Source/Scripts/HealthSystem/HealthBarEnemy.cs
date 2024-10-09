using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.HealthSystem
{
    public class HealthBarEnemy : HealthBar
    {
        [SerializeField] private float _activeTime;

        private Coroutine _disableSliderJob;
        private WaitForSeconds _wait;
        private bool _isDisabled;

        private void Start()
        {
            _wait = new WaitForSeconds(_activeTime);
        }

        protected override void HandleZeroHealth()
        {
            _isDisabled = true;
            Slider.gameObject.SetActive(false);
        }

        protected override void HandleBeforeUpdateUI()
        {
            if (_isDisabled)
                return;

            if (_disableSliderJob != null)
                StopCoroutine(_disableSliderJob);

            Slider.gameObject.SetActive(true);
        }

        protected override void HandleAfterUpdateUI()
        {
            if (_isDisabled)
                return;

            _disableSliderJob = StartCoroutine(DisableSlider());
        }

        private IEnumerator DisableSlider()
        {
            yield return _wait;
            Slider.gameObject.SetActive(false);
        }
    }
}