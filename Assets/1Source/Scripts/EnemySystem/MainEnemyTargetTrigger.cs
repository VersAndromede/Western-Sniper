using System;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class MainEnemyTargetTrigger : MonoBehaviour
    {
        [SerializeField] private Transform _targetPointForCamera;

        public Vector3 PositionForCamera => _targetPointForCamera.position;

        public Quaternion RotationForCamera => _targetPointForCamera.rotation;

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