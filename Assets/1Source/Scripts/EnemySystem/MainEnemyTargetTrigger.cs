using System;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class MainEnemyTargetTrigger : MonoBehaviour
    {
        public event Action EnemyReached;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MainEnemy mainEnemy))
            {
                mainEnemy.MakeInvulnerable();
                EnemyReached?.Invoke();
            }
        }
    }
}