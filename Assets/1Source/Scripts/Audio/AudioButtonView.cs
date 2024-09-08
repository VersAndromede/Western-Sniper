using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Audio
{
    public class AudioButtonView : MonoBehaviour
    {
        [SerializeField] private AudioButton _audioButon;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enabled;
        [SerializeField] private Sprite _disabled;

        private void OnEnable()
        {
            _audioButon.Changed += OnChanged;
        }

        private void OnDisable()
        {
            _audioButon.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _image.sprite = _audioButon.IsEnabled ? _enabled : _disabled;
        }
    }
}