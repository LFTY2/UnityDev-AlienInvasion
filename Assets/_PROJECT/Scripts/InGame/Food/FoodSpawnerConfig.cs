using UnityEngine;
using UnityEngine.Serialization;

namespace _PROJECT.Scripts.InGame.Food
{
    [CreateAssetMenu(menuName = "config/FoodSpawner")]
    public class FoodSpawnerConfig : ScriptableObject
    {
        [SerializeField] private PeopleFood _foodPrefab;
        [SerializeField] private int _maxUnits;
        [SerializeField] private float _timeBetweenSpawn;
        [SerializeField] private float _areaRadius;
        public PeopleFood FoodPrefab => _foodPrefab;
        public int MaxUnits => _maxUnits;
        public float TimeBetweenSpawn => _timeBetweenSpawn;
        public float AreaRadius => _areaRadius;

    }
}