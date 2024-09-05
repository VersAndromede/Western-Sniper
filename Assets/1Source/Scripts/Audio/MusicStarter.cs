using UnityEngine;

namespace Sctripts.Audio
{
    public class MusicStarter : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private float _startTime;
        [SerializeField] private float _delay;

        private void Start()
        {
            _musicSource.time = _startTime;
            _musicSource.PlayDelayed(_delay);
        }
    }
}