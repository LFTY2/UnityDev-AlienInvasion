using System;
using DG.Tweening;
using UnityEngine;

namespace _PROJECT.Scripts
{
    public class UIHpBar : MonoBehaviour, IDisposable
    {
        [SerializeField] private UISlider _imageSlider;
        private IDamageable _damageable;
        private Tween _tween;
        public void Initialize(IDamageable iDamageable)
        {
            _damageable = iDamageable;
            _damageable.OnHealthChange += OnHealthChange;
        }

        private void OnHealthChange(int maxHealth, int health)
        {
            _tween.Kill();
            _tween = _imageSlider.DoFillAmount((float)health / maxHealth, 0.2f);
        }

        public void Dispose()
        {
            _tween.Kill();
            _damageable.OnHealthChange -= OnHealthChange;
            _damageable = null;
            _imageSlider.FillAmount = 0;
        }
        
    }
}
