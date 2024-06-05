using UnityEngine;

namespace _PROJECT.Scripts
{
    public class AudioHandler : MonoBehaviour
    {

        [SerializeField] private Audio[] _soundAudio;
        [SerializeField] private Audio[] _musicAudio;
        [SerializeField] private AudioSource _soundAudioSource;
        [SerializeField] private AudioSource _musicAudioSource;
        private bool _isSoundOn = false;
        private bool _isMusicOn = false;

        public bool IsSoundOn => _isSoundOn;
        public bool IsMusicOn => _isMusicOn;
        
        private void Awake()
        {
            Sync();
        }

        public void PlaySound(AudioClipType audioClipType)
        {
            _soundAudioSource.Stop();
            foreach (var audio in _soundAudio)
            {
                if (audio.ClipType == audioClipType)
                {
                    _soundAudioSource.clip = audio.AudioClip;
                    break;
                }
            }
            _soundAudioSource.Play();
        }
        
        public void PlayMusic(AudioClipType audioClipType)
        {
            _musicAudioSource.Stop();
            foreach (var audio in _musicAudio)
            {
                if (audio.ClipType == audioClipType)
                {
                    _musicAudioSource.clip = audio.AudioClip;
                    break;
                }
            }
            _musicAudioSource.Play();
        }

        public void SwitchSound()
        {
            _isSoundOn = !_isSoundOn;
            Sync();
        }

        public void SwitchMusic()
        {
            _isMusicOn = !_isMusicOn;
            Sync();
        }

        private void Sync()
        {
            _soundAudioSource.volume = _isSoundOn ? 0.1f : 0;
            _musicAudioSource.volume = _isMusicOn ? 0.05f : 0;
        }
    }
}