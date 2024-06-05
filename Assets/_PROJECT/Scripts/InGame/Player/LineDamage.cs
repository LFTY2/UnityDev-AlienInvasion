using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _PROJECT.Scripts.InGame.Player
{
    public class LineDamage : MonoBehaviour, IDisposable
    {
        [Inject] private Timer _timer;
        private Transform _target;
        [SerializeField] private Transform _centerTransform;
         [SerializeField] private Transform _capsuleTransform;

        public void Initialize(Transform target)
        {
            _target = target;
            _timer.OnTick += OnTick;
        }

        private void OnTick()
        {
            if (_target == null) return;
            Vector3 targetPos = _target.position;
            Vector3 centerPosition = transform.position;
            
            Vector3 directionToTarget = targetPos - centerPosition;
            float angle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
            _centerTransform.rotation = Quaternion.Euler(90, angle, 0);

            Vector3 scale = _centerTransform.localScale;
            scale.y = directionToTarget.magnitude;
            _centerTransform.localScale = scale;
        }

        public void Dispose()
        {
            _timer.OnTick -= OnTick;
            _target = null;
        }
    }
}