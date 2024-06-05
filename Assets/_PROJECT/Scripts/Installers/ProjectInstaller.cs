using _PROJECT.Scripts.InGame;
using _PROJECT.Scripts.InGame.Bank;
using _PROJECT.Scripts.UI;
using Core;
using Plugins.Joystick.Scripts;
using UnityEngine;
using Zenject;

namespace _PROJECT.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private UIHandler _uiHandler;
        [SerializeField] private AudioHandler _audioHandler;
        [SerializeField] private Timer _timer;
        [SerializeField] private Joystick _joystick;
        public override void InstallBindings()
        {
            Container.Bind<SceneHandler>().AsSingle().NonLazy();
            Container.Bind<FoodVault>().AsSingle().NonLazy();
            
            Container.Bind<UIHandler>().FromInstance(_uiHandler).AsSingle().NonLazy();
            Container.Bind<AudioHandler>().FromInstance(_audioHandler).AsSingle().NonLazy();
            Container.Bind<Timer>().FromInstance(_timer).AsSingle().NonLazy();
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle().NonLazy();
        }
    }
}