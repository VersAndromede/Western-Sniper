using UnityEngine;
using VContainer;

namespace Scripts.EnemySystem.EnemyCounterSystem
{
    public class EnemyCounterHandler : MonoBehaviour
    {
        [SerializeField] private EnemyCounterView _view;

        private EnemyCounter _enemyCounter;

        private void OnDestroy()
        {
            _enemyCounter.Unsubscribe();
            _enemyCounter.Changed -= OnChanged;
        }

        private void UpdateView()
        {
            _view.UpdateCount(_enemyCounter.KilledCount, _enemyCounter.MaxCount);
        }

        private void OnChanged()
        {
            UpdateView();
        }

        [Inject]
        private void Construct(EnemyCounter enemyCounter)
        {
            _enemyCounter = enemyCounter;
            UpdateView();

            _enemyCounter.Changed += OnChanged;
        }
    }
}