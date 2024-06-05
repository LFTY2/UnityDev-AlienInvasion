using UnityEngine;

namespace _PROJECT.Scripts.InGame.Player
{
    [CreateAssetMenu(menuName = "config/PlayerMovement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private float _rotationSpeed = 100;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
    }
}