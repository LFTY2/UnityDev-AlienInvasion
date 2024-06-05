using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _PROJECT.Scripts.UI
{
    public class SceneHandler
    {
        [Inject] private UIHandler _uiHandler;
        public enum SceneType
        {
            Boot,
            Game
        }
        
        public void LoadScene(SceneType sceneType)
        {
            LoadSceneAsync((int)sceneType);
        }

        private async void LoadSceneAsync(int buildIndex)
        {
            _uiHandler.UISceneChange.Open();
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
            _uiHandler.UISceneChange.Close();
        }

        public void ReloadCurrentScene()
        {
            LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}