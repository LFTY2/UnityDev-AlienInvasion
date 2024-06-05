using System;
using Core;
using UnityEngine;
using Zenject;

namespace _PROJECT.Scripts
{
    public class UILookAtCamera : MonoBehaviour
    {
        [Inject] private Timer _timer;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _timer.OnTick += LockRotation;
        }

        private void LockRotation()
        {
            _transform.rotation = Quaternion.identity;
        }

        private void OnDisable()
        {
            _timer.OnTick -= LockRotation;
        }
    }
}