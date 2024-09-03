using UnityEngine;

namespace Scripts.ShootingSystem
{
    public class PlayerWeaponView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private ParticleSystem _particleSystemPrefab;

        public void Play(Vector3 hitPoint)
        {
            _audioSource.Play();
            ParticleSystem particleSystem = Instantiate(_particleSystemPrefab);
            particleSystem.transform.position = hitPoint;
            particleSystem.Play();
        }
    }
}