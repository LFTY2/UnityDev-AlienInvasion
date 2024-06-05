using System;
using DG.Tweening;
using UnityEngine;

namespace _PROJECT.Scripts
{
    public class UILoading : UIMenuBase
    {
        [SerializeField] private UISlider _uiSlider;
        public event Action OnLoadingOver;

        public void StartLoading()
        {
            _uiSlider.FillAmount = 0;
            _uiSlider.DoFillAmount(1, 1f).OnComplete(OnLoadComplete);
        }

        private void OnLoadComplete()
        {
            Close();
            OnLoadingOver?.Invoke();
        }
    }
}