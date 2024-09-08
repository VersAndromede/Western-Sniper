using Scripts.EnemySystem;
using UnityEngine;

namespace EnemyCounterSystem
{
    public class EnemyCounterHandler : MonoBehaviour
    {
        [SerializeField] private EnemyCounterView[] _views;
        [SerializeField] private Enemy[] _enemies;

        private EnemyCounter _enemyCounter;

        private void OnValidate()
        {
            _enemies = GetComponentsInChildren<Enemy>(true);
        }

        private void Start()
        {
            _enemyCounter = new EnemyCounter(_enemies);
            UpdateView();

            _enemyCounter.Changed += OnChanged;
        }

        private void OnDestroy()
        {
            _enemyCounter.Unsubscribe();
            _enemyCounter.Changed -= OnChanged;
        }

        private void UpdateView()
        {
            foreach (EnemyCounterView view in _views)
                view.UpdateCount(_enemyCounter.KilledCount, _enemyCounter.MaxCount);
        }

        private void OnChanged()
        {
            UpdateView();
        }
    }
}