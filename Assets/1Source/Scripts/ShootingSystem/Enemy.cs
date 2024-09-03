using Scripts.HealthSystem;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyEffector _effector;

        private Health _health;

        private void OnDestroy()
        {
            _health.Over -= OnOver;
        }

        public void Init(Health health)
        {
            _health = health;

            _health.Over += OnOver;
        }

        public void TakeDamage()
        {
            _health.TakeDamage(35);
        }

        private void OnOver()
        {
            _effector.PlayDeth();
        }
    }
}
