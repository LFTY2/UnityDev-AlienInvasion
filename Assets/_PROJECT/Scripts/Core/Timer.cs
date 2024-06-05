using System;
using UnityEngine;

namespace Core
{
    public sealed class Timer : MonoBehaviour
    {
        public event Action OnTick;
        public event Action OnPostTick;
        public event Action OnFixedTick;
        public event Action OnSecondTick;
        

        private float _unscaledTime;
        private float _lastTime;
        private float _deltaTime;
        private float _scaleTime;
        private float _time;

        public void Awake()
        {
            _lastTime = GetTime();
            _scaleTime = 1f;
            _deltaTime = 0f;
            _time = 0f;
        }

        public float Time => _time;
        public float DeltaTime => _deltaTime;
        public float TimeScale 
        { 
            get => _scaleTime;
            set => _scaleTime = Math.Max(0f, value);
        }

        public void Update()
        {
            var now = GetTime();
            var delta = now - _lastTime;
            _unscaledTime += delta;
            _deltaTime = delta * TimeScale;
            _time += _deltaTime;

            bool isNewSecondTick = Mathf.Floor(now) > Mathf.Floor(_lastTime);

            _lastTime = now;

            OnTick?.Invoke();

            if (isNewSecondTick)
            {
                OnSecondTick?.Invoke();
            }
        }

        public void LateUpdate()
        {
            OnPostTick?.Invoke();
        }

        public void FixedUpdate()
        {
            OnFixedTick?.Invoke();
        }

        private float GetTime()
        {
            return Environment.TickCount / 1000f;
        }
    }
}