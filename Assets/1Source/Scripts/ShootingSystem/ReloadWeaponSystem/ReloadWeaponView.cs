using DG.Tweening;
using Scripts.GameConfigSystem;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Scripts.ShootingSystem.ReloadWeaponSystem
{
    public class ReloadWeaponView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private GameObject _container;
        [SerializeField] private AnimationCurve _sliderChangeCurve;
        [SerializeField] private AudioSource _audioSource;

        private GameConfig _gameConfig;

        public void AnimateSlider()
        {
            const int FullValue = 1;

            _slider.value = 0;
            _container.SetActive(true);
            _slider.DOValue(FullValue, _gameConfig.PlayerWeaponConfig.PreReloadTime)
                .SetEase(_sliderChangeCurve)
                .OnComplete(EndAnimation);
        }

        [Inject]
        private void Construct(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        private void EndAnimation()
        {
            DOVirtual.DelayedCall(_gameConfig.PlayerWeaponConfig.ReloadTime, () =>
            {
                _audioSource.Play();
                _container.SetActive(false);
            });
        }
    }
}