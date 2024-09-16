using UnityEngine;

namespace Scripts.EnemySystem
{

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _speed;
        [SerializeField] private float _maxLifetime;
        [SerializeField] private float _dethDuration;
        [SerializeField] private AnimationCurve _animationCurve;

        private void Start()
        {
            Destroy(gameObject, _maxLifetime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out BulletCatcher bulletCatcher))
            {
                _rigidbody.isKinematic = true;
                ParticleSystem particle = Instantiate(_particleSystem, transform);
                particle.transform.position = _rigidbody.position;
                particle.Play();
                bulletCatcher.Catch();
                Destroy(_renderer);
                Destroy(gameObject, _dethDuration);
            }
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity * _speed;
        }
    }
}
