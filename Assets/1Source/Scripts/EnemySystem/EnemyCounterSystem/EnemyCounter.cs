using System;

namespace Scripts.EnemySystem.EnemyCounterSystem
{
    public class EnemyCounter
    {
        private Enemy[] _enemies;

        public int MaxCount { get; private set; }

        public int KilledCount { get; private set; }

        public event Action Changed;

        public event Action AllDied;

        public void Init(Enemy[] enemies)
        {
            _enemies = enemies;
            MaxCount = enemies.Length;

            foreach (Enemy enemy in _enemies)
                enemy.Died += OnDied;
        }

        public void Unsubscribe()
        {
            foreach (Enemy enemy in _enemies)
                enemy.Died -= OnDied;
        }

        private void OnDied()
        {
            KilledCount++;
            Changed?.Invoke();

            if (KilledCount == MaxCount)
                AllDied?.Invoke();
        }
    }
}