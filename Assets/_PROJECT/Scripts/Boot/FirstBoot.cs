using System;
using _PROJECT.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _PROJECT.Scripts.Boot
{
    public class FirstBoot : MonoBehaviour
    {
        [Inject] private UIHandler _uiHandler;
        [Inject] private SceneHandler _sceneHandler;
        private void Awake()
        {
            _uiHandler.UILoading.Open();
            _uiHandler.UILoading.StartLoading();
            _uiHandler.UILoading.OnLoadingOver += OpenStartMenu;
        }

        private void OpenStartMenu()
        {
            _sceneHandler.LoadScene(SceneHandler.SceneType.Game);
            _uiHandler.UILoading.OnLoadingOver -= OpenStartMenu;
        }
    }
}