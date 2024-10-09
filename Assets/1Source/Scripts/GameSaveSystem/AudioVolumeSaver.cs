using Scripts.Audio;
using System;
using UnityEngine;
using VContainer;

namespace Modules.SavingsSystem
{
    public class AudioVolumeSaver : IDisposable
    {
        private readonly AudioButton _audioButton;
        private readonly SaveSystem _saveSystem;

        [Inject]
        public AudioVolumeSaver(AudioButton audioButton, SaveSystem saveSystem)
        {
            _audioButton = audioButton;
            _saveSystem = saveSystem;
            _audioButton.Init(_saveSystem.Load().MusicEnabled);

            _audioButton.Changed += OnChanged;
        }

        public void Dispose()
        {
            _audioButton.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _saveSystem.Save(data =>
            {
                data.MusicEnabled = _audioButton.IsEnabled;
            });
        }
    }
}