using UnityEngine;

namespace LastChild
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _bgMusic;
        [SerializeField] private float _volume = 0.5f;

        private AudioSource _audioSource;

        private int _clipIndex;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            _audioSource.volume = _volume;

            _audioSource.clip = _bgMusic[_clipIndex];

            _audioSource.Play();
        }

        private void Update()
        {
            if (_audioSource.isPlaying == false)
            {
                _clipIndex++;

                if (_clipIndex < _bgMusic.Length)
                {
                    _audioSource.clip = _bgMusic[_clipIndex];
                }
                else
                {
                    _clipIndex = 0;
                    _audioSource.clip = _bgMusic[_clipIndex];
                }

                _audioSource.Play();
            }
        }
    }
}
