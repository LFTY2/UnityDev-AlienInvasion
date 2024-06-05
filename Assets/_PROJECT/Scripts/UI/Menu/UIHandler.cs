using _PROJECT.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _PROJECT.Scripts
{
    public class UIHandler : MonoBehaviour
    {
        [Inject] private SceneHandler _sceneHandler;
        [SerializeField] private UILoading _uiLoading;
        [SerializeField] private UIPopUpMenu _uiPopUpMenu;
        [SerializeField] private UISceneChange _uiSceneChange;
        
        [SerializeField] private UIGame _uiGame;
        [SerializeField] private UISettings _uiSettings;

        public UILoading UILoading => _uiLoading;
        
        public UIPopUpMenu PopUpMenu => _uiPopUpMenu;
        public UIGame UIGame => _uiGame;
        public UISettings UISettings => _uiSettings;
        public UISceneChange UISceneChange => _uiSceneChange;

    }
}