using Scripts.HealthSystem;
using System;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyEffector _effector;

        private Health _health;
        private bool _isDead;

        public event Action Died;

        private void OnDestroy()
        {
            _health.Over -= OnOver;
        }

        public void Init(Health health)
        {
            _health = health;

            _health.Over += OnOver;
        }

        public void TakeDamage(uint damage)
        {
            if (_isDead)
            {
                _effector.PlayDeth();
                return;
            }

            _health.TakeDamage(damage);

            if (_effector.HatFlewOff == false)
                _effector.ThrowHat();
        }

        public void PlayHeadshot()
        {
            if (_isDead == false)
                _effector.PlayHeadshot();
        }

        private void OnOver()
        {
            _isDead = true;
            Died?.Invoke();
            _effector.PlayDeth();
        }
    }
}
