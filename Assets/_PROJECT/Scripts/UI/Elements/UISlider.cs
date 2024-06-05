using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _PROJECT.Scripts
{
    public class UISlider : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] private float _fillAmount = 0.5f;
        [SerializeField] private Image _fillImage;

        public float FillAmount
        {
            get => _fillAmount;

            set
            {
                _fillAmount = value;
                _fillImage.fillAmount = _fillAmount;
            }
        }

        public Tween DoFillAmount(float endValue, float duration)
        {
            return _fillImage.DOFillAmount(endValue, duration);
        }

        private void OnValidate()
        {
            _fillImage.fillAmount = _fillAmount;
        }
    }
}