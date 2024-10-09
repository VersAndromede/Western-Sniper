using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.EnemySystem
{
    public class DieEffect : MonoBehaviour
    {
        [SerializeField] private Animator _enemyAnimator;

        private List<Rigidbody> _rigidbodies;

        private void Start()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>(true).ToList();

            if (TryGetComponent(out Enemy enemy))
                if (enemy.TryGetComponent(out Rigidbody rigidbody))
                    _rigidbodies.Remove(rigidbody);

            foreach (Rigidbody rigidbody in _rigidbodies)
                rigidbody.isKinematic = true;
        }

        public void Play()
        {
            _enemyAnimator.enabled = false;

            foreach (Rigidbody rigidbody in _rigidbodies)
                rigidbody.isKinematic = false;
        }
    }
}
