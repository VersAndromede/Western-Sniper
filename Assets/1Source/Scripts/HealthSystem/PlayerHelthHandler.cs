using Scripts.EnemySystem;
using System;
using UnityEngine;

namespace Scripts.HealthSystem
{
    public class PlayerHelthHandler : MonoBehaviour
    {
        private const uint EnemyDamage = 10;

        [SerializeField] private int _count;
        [SerializeField] private HealthBarPlayer _healthBar;
        [SerializeField] private PlayerDamageEffect _playerDamageEffect;

        private BulletCatcher[] _bulletCatchers;
        private HealthPresenter _healthPresenter;
        private Health _health;

        public event Action Died;

        private void Start()
        {
            _health = new Health(_count);
            _healthPresenter = new HealthPresenter(_health, _healthBar);
            _bulletCatchers = GetComponentsInChildren<BulletCatcher>(true);

            foreach (BulletCatcher bulletCatcher in _bulletCatchers)
                bulletCatcher.Catched += OnCatched;
        }

        private void OnDestroy()
        {
            _healthPresenter.Unsubscribe();

            foreach (BulletCatcher bulletCatcher in _bulletCatchers)
                bulletCatcher.Catched -= OnCatched;
        }

        private void OnCatched()
        {
            _health.TakeDamage(EnemyDamage);
            _playerDamageEffect.Play();

            if (_health.Count == 0)
                Died?.Invoke();
        }
    }
}
