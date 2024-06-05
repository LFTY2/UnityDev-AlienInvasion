using UnityEngine;

namespace _PROJECT.Scripts.InGame.Food
{
    [CreateAssetMenu(menuName = "config/Food")]
    public class FoodConfig : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _foodGive;
        [SerializeField] private float _timeChangePos = 3; 
        public int Health => _health;
        public int FoodGive => _foodGive;
        public float TimeChangePos => _timeChangePos;
    }
}