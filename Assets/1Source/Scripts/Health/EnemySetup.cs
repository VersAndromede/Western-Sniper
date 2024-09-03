using Scripts.EnemySystem;
using UnityEngine;

namespace Scripts.HealthSystem
{
    public class EnemySetup : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private int _maxCount;
        [SerializeField] private HealthBar _bar;

        private HealthPresenter _healthPresenter;

        private void Start()
        {
            Health health = new Health(_maxCount);
            _healthPresenter = new HealthPresenter(health, _bar);
            _enemy.Init(health);
        }

        private void OnDestroy()
        {
            _healthPresenter.Unsubscribe();
        }
    }
}