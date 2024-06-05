using System;
using _PROJECT.Scripts.InGame.Bank;
using Camera;
using Plugins.Joystick.Scripts;
using UnityEngine;
using Zenject;

namespace _PROJECT.Scripts
{
    public class InGameHandlerBase : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerControllerPrefab;
        [Inject] private Joystick _joystick;
        [Inject] private DiContainer _diContainer;
        [Inject] private AudioHandler _audioHandler;
        [Inject] private UIHandler _uiHandler;
        [Inject] private CameraHandler _cameraHandler;
        private PlayerController _playerController;
        public PlayerController PlayerController => _playerController;
        
        private void Awake()
        {
            _audioHandler.PlayMusic(AudioClipType.BGGame);
            _uiHandler.UIGame.Open();
            _playerController = _diContainer.InstantiatePrefab(_playerControllerPrefab, Vector3.zero, Quaternion.identity, null).GetComponent<PlayerController>();
            _uiHandler.UIGame.UIHpBar.Initialize(_playerController);
            _playerController.Initialize();
            _cameraHandler.SetTarget(_playerController.transform);
            _joystick.enabled = true;
        }
        
        public void DamagePlayer(int damageNum)
        {
            _playerController.TakeDamage(damageNum);
        }
        public void HealPlayer(int healNum)
        {
            _playerController.Heal(healNum);
        }
        private void OnDestroy()
        {
            _playerController.Dispose();
            _joystick.enabled = false;
        }
    }
}