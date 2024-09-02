using UnityEngine;

namespace Scripts.EnemySystem.Body
{
    public class EnemyEffector : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Vector2 _randomXForce;
        [SerializeField] private Vector2 _randomYForce;
        [SerializeField] private Vector2 _randomZForce;
        [SerializeField] private float _multiplierForForceToHat;
        
        private Rigidbody[] _rigidbodies;
        private bool _hatFlewOff;

        private void Start()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>(true);

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;

                if (rigidbody.TryGetComponent(out EnemyPart _) == false)
                    rigidbody.gameObject.AddComponent<EnemyPart>();
            }
        }

        public void PlayDeth()
        {
            _animator.enabled = false;

            foreach (Rigidbody rigidbody in _rigidbodies)
                rigidbody.isKinematic = false;

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                if (rigidbody.TryGetComponent(out EnemyPart part))
                {
                    if (part.Type == EnemyPartType.Body)
                        AddForce(rigidbody);

                    if (part.Type == EnemyPartType.Hat && _hatFlewOff == false)
                    {
                        rigidbody.transform.SetParent(null);
                        AddForce(rigidbody, _multiplierForForceToHat);
                        _hatFlewOff = true;
                    }
                }
            }
        }

        private void AddForce(Rigidbody rigidbody, float multiplier = 1)
        {
            float xForce = Random.Range(_randomXForce.x, _randomXForce.y);
            float yForce = Random.Range(_randomYForce.x, _randomYForce.y);
            float zForce = Random.Range(_randomZForce.x, _randomZForce.y);

            Vector3 force = new Vector3(xForce, yForce, zForce) * multiplier;
            rigidbody.AddForce(force, ForceMode.Impulse);
        }
    }
}
