using System;
using Modules.SavingsSystem;
using VContainer;

namespace Scripts.LevelSystem
{
    public class LevelSaver : IDisposable
    {
        private readonly SaveSystem _saveSystem;
        private readonly Level _level;

        [Inject]
        public LevelSaver(SaveSystem saveSystem, Level level)
        {
            _saveSystem = saveSystem;
            _level = level;

            _level.Changed += OnChanged;
        }

        public void Dispose()
        {
            _level.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _saveSystem.Save(data =>
            {
                data.Level = _level;
            });
        }
    }
}