using Scripts.HealthSystem;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class EnemySetup : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private int _maxCount;
        [SerializeField] private HealthBarEnemy _bar;

        private HealthPresenter _healthPresenter;

        private void Start()
        {
            Health health = new (_maxCount);
            _healthPresenter = new HealthPresenter(health, _bar);
            _enemy.Init(health);
        }

        private void OnDestroy()
        {
            _healthPresenter.Unsubscribe();
        }
    }
}