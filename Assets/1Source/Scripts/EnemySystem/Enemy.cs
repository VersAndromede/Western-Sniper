using System;
using Scripts.HealthSystem;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyAlgorithm _algorithm;
        [SerializeField] private EnemyEffector _effector;

        private Health _health;
        private bool _isDied;
        private bool _isInvulnerable;

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
            if (_isInvulnerable)
                return;

            if (_isDied)
            {
                _effector.PlayDeth();
                return;
            }

            _health.TakeDamage(damage);

            if (_effector.HatFlewOff == false)
                _effector.ThrowHat();
        }

        public void RunAlgorithm()
        {
            if (_isDied == false)
                _algorithm?.Run();
        }

        public void MakeInvulnerable()
        {
            _isInvulnerable = true;
        }

        public void PlayHeadshot()
        {
            if (_isInvulnerable || _isDied)
                return;

            _effector.PlayHeadshot();
        }

        private void OnOver()
        {
            _isDied = true;
            _algorithm?.Stop();
            Died?.Invoke();
            _effector.PlayDeth();
        }
    }
}
