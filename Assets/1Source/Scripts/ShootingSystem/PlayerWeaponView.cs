using UnityEngine;

namespace Scripts.ShootingSystem
{
    public class PlayerWeaponView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play()
        {
            _audioSource.Play();
        }
    }
}