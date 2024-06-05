using Camera;
using Plugins.Joystick.Scripts;
using UnityEngine;
using Zenject;

namespace _PROJECT.Scripts.Installers
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private InGameHandlerBase _inGameHandler;
        [SerializeField] private CameraHandler _cameraHandler;
        
        public override void InstallBindings()
        {
            Container.Bind<InGameHandlerBase>().FromInstance(_inGameHandler).AsSingle().NonLazy();
            Container.Bind<CameraHandler>().FromInstance(_cameraHandler).AsSingle().NonLazy();
            
        }
    }
}