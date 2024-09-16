using Scripts.HealthSystem;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class PlayerHelthHandler : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private HealthBar _healthBar;

        private BulletCatcher[] _bulletCatchers;
        private HealthPresenter _healthPresenter;
        private Health _health;

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
            _health.TakeDamage(10);
        }
    }
}
