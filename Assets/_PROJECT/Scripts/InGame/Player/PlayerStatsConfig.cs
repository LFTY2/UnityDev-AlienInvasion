using UnityEngine;


namespace _PROJECT.Scripts.InGame.Player
{
    [CreateAssetMenu(menuName = "config/PlayerStats")]
    public class PlayerStatsConfig : ScriptableObject
    {
        [Header("Change this for testing")]
        [SerializeField] private int _targetsHitMax = 1;
        
        [Header("Other")]
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _health = 100;
        [SerializeField] private int _maxFood = 10;
        [SerializeField] private float _hitRadius = 2;
       
        public int Damage => _damage;
        public int Health => _health;
        public int MaxFood => _maxFood;
        public float HitRadius => _hitRadius;
        public int TargetHitMax => _targetsHitMax;
    }
}