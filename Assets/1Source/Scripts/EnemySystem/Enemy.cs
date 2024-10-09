using System;
using ExplodingBarrelSystem;
using Scripts.HealthSystem;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class Enemy : SubjectToExplosion
    {
        [SerializeField] private EnemyAlgorithm _algorithm;
        [SerializeField] private EnemyEffector _effector;

        private Health _health;
        private bool _isDied;
        private bool _isInvulnerable;

        public Rigidbody Body => _effector.Body;

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

            _health.TakeDamage(damage);

            if (_effector.HatFlewOff == false)
                _effector.ThrowHat();

            if (_isDied)
            {
                _effector.AddBodyForce();
                return;
            }
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

        public void Die()
        {
            if (_isDied)
                return;

            _isDied = true;
            _algorithm?.Stop();
            Died?.Invoke();
            _effector.PlayDeth();
        }

        private void OnOver()
        {
            Die();
        }
    }
}
