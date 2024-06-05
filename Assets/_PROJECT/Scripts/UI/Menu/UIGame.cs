using _PROJECT.Scripts.InGame.Bank;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _PROJECT.Scripts
{
    public class UIGame : UIMenuBase
    {
        [Inject] private AudioHandler _audioHandler;
        [Inject] private UIHandler _uiHandler;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private UIHpBar _uiHpBar;
        public UIHpBar UIHpBar => _uiHpBar;
        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(OpenPause);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(OpenPause);
            _uiHpBar.Dispose();
        }

        private void OpenPause()
        {
            _uiHandler.UISettings.Open();
            _audioHandler.PlaySound(AudioClipType.UIClick);
        }
    }
}