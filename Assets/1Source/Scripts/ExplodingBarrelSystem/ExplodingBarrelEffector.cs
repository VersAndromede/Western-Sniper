using UnityEngine;

namespace ExplodingBarrelSystem
{
    public class ExplodingBarrelEffector : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private AudioSource _audioSource;

        public void Animate()
        {
            _particleSystem.transform.SetParent(null);
            _particleSystem.Play();
            _audioSource.Play();
            Destroy(gameObject);
        }
    }
}