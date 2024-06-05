using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _PROJECT.Scripts
{
    public class UIPopUpMenu : UIMenuBase
    {
        [Inject] private AudioHandler _audioHandler;
        [SerializeField] private TMP_Text _massageText;
        [SerializeField] protected Button _close;

        protected void OnEnable()
        {
            _close.onClick.AddListener(CloseMenu);
        }
        protected void OnDisable()
        {
            _close.onClick.RemoveListener(CloseMenu);
        }
        
        public void Open(string massage)
        {
            Open();
            _massageText.text = massage;
        }
        
        private void CloseMenu()
        {
            _audioHandler.PlaySound(AudioClipType.UIClick);
            Close();
        }
    }
}