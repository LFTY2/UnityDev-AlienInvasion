using System;
using Camera;
using Plugins.Joystick.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Timer = Core.Timer;

namespace _PROJECT.Scripts.InGame.Player
{
    public class PlayerMovement : MonoBehaviour, IDisposable
    {
        [Inject] private Joystick _joystick;
        [Inject] private CameraHandler _cameraHandler;
        [Inject] private Timer _timer;
        [SerializeField] private PlayerMovementConfig _playerMovementConfig;
        [SerializeField] private NavMeshAgent _agent;
        private float _walkSpeed;
        private float _rotateSpeed;
        private Transform _transform;

        public void Initialize()
        {
            _agent.enabled = true;

            _walkSpeed = _playerMovementConfig.MoveSpeed;
            _rotateSpeed = _playerMovementConfig.RotationSpeed;
            _transform = transform;

            _timer.OnTick += OnTick;
        }

        public void Dispose()
        {
            _timer.OnTick -= OnTick;
        }

        private void OnTick()
        {
            if (!_joystick.IsTouched) return;
          
            var cameraEulerY = _cameraHandler.transform.localEulerAngles.y;

            var joystickVector = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            var angle = (Mathf.Atan2(_joystick.Horizontal, _joystick.Vertical) * Mathf.Rad2Deg) +
                        cameraEulerY;

            var localEulerAngles = _transform.localEulerAngles;
            var deltaAngle = Mathf.Abs(Mathf.DeltaAngle(localEulerAngles.y, angle)) / 90f;
            deltaAngle = 1 - Mathf.Clamp01(deltaAngle);

            angle = Mathf.LerpAngle(localEulerAngles.y, angle,
                Time.deltaTime * _rotateSpeed * joystickVector.sqrMagnitude);
            localEulerAngles = new Vector3(0f, angle, localEulerAngles.z);
            _transform.localEulerAngles = localEulerAngles;

            Vector3 direction = _transform.forward;
            var speed = _walkSpeed * deltaAngle * joystickVector.magnitude;
            _transform.position += direction.normalized * (Time.deltaTime * speed);
            
        }

    }
}