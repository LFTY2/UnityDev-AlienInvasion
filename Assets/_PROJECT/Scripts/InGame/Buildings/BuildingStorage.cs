using System;
using _PROJECT.Scripts.InGame.Player;
using UnityEngine;
using Zenject;

namespace _PROJECT.Scripts.InGame.Buildings
{
    public class BuildingStorage : MonoBehaviour, IIntractable, IRadius
    {
        public event Action<float> OnRadiusChange;
        [Inject] private InGameHandlerBase _gameHandler;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private RadiusDisplay _radiusDisplay;
        public float Radius => _sphereCollider.radius;

        private void Start()
        {
            _radiusDisplay.Initialize(this);
            InvokeRadiusChange();
        }

        public void Interact()
        {
            _gameHandler.PlayerController.StoreFood();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _sphereCollider.radius);
        }

        private void InvokeRadiusChange()
        {
            OnRadiusChange?.Invoke(Radius);
        }

        private void OnDestroy()
        {
            _radiusDisplay.Dispose();
        }
    }
}