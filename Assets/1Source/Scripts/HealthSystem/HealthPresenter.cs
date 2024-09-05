using UnityEngine;

namespace Scripts.HealthSystem
{
    public class HealthPresenter
    {
        private readonly Health _health;
        private readonly HealthBar _bar;

        public HealthPresenter(Health health, HealthBar bar)
        {
            _health = health;
            _bar = bar;

            _health.Changed += OnChanged;
        }

        public void Unsubscribe()
        {
            _health.Changed -= OnChanged;
        }

        private void OnChanged()
        {
            _bar.UpdateUI(_health.Count, _health.MaxCount);
        }
    }
}