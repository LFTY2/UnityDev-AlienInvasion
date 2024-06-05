using UnityEngine;

namespace _PROJECT.Scripts.InGame.Player
{
    public class RadiusDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _scaleTransform;
        private IRadius _radius;

        public void Initialize(IRadius radius)
        {
            _radius = radius;
            _radius.OnRadiusChange += ChangeFoodText;
        }

        private void ChangeFoodText(float radius)
        {
            _scaleTransform.localScale = Vector3.one * _radius.Radius;
        }

        public void Dispose()
        {
            _radius.OnRadiusChange -= ChangeFoodText;
            _radius = null;
        }
    }
}