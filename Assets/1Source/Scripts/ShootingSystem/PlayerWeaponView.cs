using DG.Tweening;
using UnityEngine;

namespace Scripts.ShootingSystem
{
    public class PlayerWeaponView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private AudioSource _shot;
        [SerializeField] private AudioSource _ammunitionChange;
        [SerializeField] private float _ammunitionChangeClipDaley;
        [SerializeField] private ParticleSystem _particleSystemPrefab;

        public void Play(Vector3 hitPoint, bool ammunitionEmpty)
        {
            PlayAudio(_shot);
            ParticleSystem particleSystem = Instantiate(_particleSystemPrefab);
            particleSystem.transform.position = hitPoint;
            particleSystem.Play();

            if (ammunitionEmpty == false)
                PlayAudio(_ammunitionChange, _ammunitionChangeClipDaley);
        }

        private void PlayAudio(AudioSource audioSource, float daley = 0)
        {
            audioSource.PlayDelayed(daley);
        }
    }
}