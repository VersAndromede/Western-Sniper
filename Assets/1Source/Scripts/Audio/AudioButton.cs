using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Scripts.Audio
{
    public class AudioButton : MonoBehaviour
    {
        private const string VolumeParameter = "Volume";
        private const int FullVolume = 0;
        private const int ZeroVolume = -80;

        [SerializeField] private AudioMixer _audioMixer;

        public bool IsEnabled { get; private set; } = true;

        public event Action Changed;

        public void Switch()
        {
            if (IsEnabled)
                Disable();
            else
                Enable();
        }

        private void Enable()
        {
            _audioMixer.SetFloat(VolumeParameter, FullVolume);
            IsEnabled = true;
            Changed?.Invoke();
        }

        private void Disable()
        {
            _audioMixer.SetFloat(VolumeParameter, ZeroVolume);
            IsEnabled = false;
            Changed?.Invoke();
        }
    }
}