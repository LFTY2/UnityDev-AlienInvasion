﻿using UnityEngine;

namespace Camera
{
    public sealed class CameraHandler : MonoBehaviour
    {
        private const float _angleY = 45f;
        private const float _speedPosition = 5f;
        private const float _speedRotation = 3f;
        private const float _duration = 2f;

        [SerializeField] private UnityEngine.Camera _camera;

        private float _timer;
        private float _zInitialPosition;
        private bool _isAutoZoomOut;
        private Transform _prevTarget;
        private Transform _target;

        public UnityEngine.Camera Camera => _camera;
        private int _sign;

        private void Awake()
        {
            _zInitialPosition = _camera.transform.localPosition.z;
            _sign = 0;
        }

        private void OnEnable()
        {
            transform.position = Vector3.zero;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _speedPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, _angleY * _sign, transform.rotation.z), Time.deltaTime * _speedRotation);

            if (!_isAutoZoomOut) return;

            _timer += Time.deltaTime;
            if (_timer >= _duration)
            {
                _timer = 0f;
                _isAutoZoomOut = false;

                _target = _prevTarget;
                
            }
        }

        public void SetTarget(Transform target)
        {
            _prevTarget = _target;
            _target = target;
        }

        public void ZoomIn(bool isAutoZoomOut)
        {
            _isAutoZoomOut = isAutoZoomOut;
            
        }

        public void ZoomOut()
        {
            var cameraTransform = _camera.transform;
            Vector3 pos = cameraTransform.position;
            pos.z = _zInitialPosition;
            cameraTransform.position = pos;
        }

        internal void SetSign(int sign)
        {
            _sign = sign;
        }
    }
}