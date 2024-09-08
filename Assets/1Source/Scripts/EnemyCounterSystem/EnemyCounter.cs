using Scripts.EnemySystem;
using System;

namespace EnemyCounterSystem
{
    public class EnemyCounter
    {
        private readonly Enemy[] _enemies;

        public EnemyCounter(Enemy[] enemies)
        {
            _enemies = enemies;
            MaxCount = _enemies.Length;

            foreach (Enemy enemy in _enemies)
                enemy.Died += OnDied;
        }

        public int MaxCount { get; private set; }

        public int KilledCount { get; private set; }

        public event Action Changed;

        public void Unsubscribe()
        {
            foreach (Enemy enemy in _enemies)
                enemy.Died -= OnDied;
        }

        private void OnDied()
        {
            KilledCount++;
            Changed?.Invoke();
        }
    }
}