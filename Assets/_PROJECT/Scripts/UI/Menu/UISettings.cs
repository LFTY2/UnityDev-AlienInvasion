using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _PROJECT.Scripts
{
    public class UISettings : UIMenuBase
    {
        [Inject] private AudioHandler _audioHandler;
        [Inject] private UIHandler _uiHandler;
        [SerializeField] private Button _soundToggle;
        [SerializeField] private Button _musicToggle;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _soundOn;
        [SerializeField] private GameObject _soundOff;
        [SerializeField] private GameObject _musicOn;
        [SerializeField] private GameObject _musicOff;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseMenu);
            _soundToggle.onClick.AddListener(SwitchSound);
            _musicToggle.onClick.AddListener(SwitchMusic);
            SyncView();
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseMenu);
            _soundToggle.onClick.RemoveListener(SwitchSound);
            _musicToggle.onClick.RemoveListener(SwitchMusic);
        }

        private void SwitchSound()
        {
            _audioHandler.PlaySound(AudioClipType.UIClick);
            _audioHandler.SwitchSound();
            SyncView();
        }
        
        private void SwitchMusic()
        {
            _audioHandler.PlaySound(AudioClipType.UIClick);
            _audioHandler.SwitchMusic();
            SyncView();
        }

        private void SyncView()
        {
            _soundOn.SetActive(_audioHandler.IsSoundOn);
            _soundOff.SetActive(!_audioHandler.IsSoundOn);
            _musicOn.SetActive(_audioHandler.IsMusicOn);
            _musicOff.SetActive(!_audioHandler.IsMusicOn);
        }
        
        private void CloseMenu()
        {
            _audioHandler.PlaySound(AudioClipType.UIClick);
            Close();
        }
        
    }
}