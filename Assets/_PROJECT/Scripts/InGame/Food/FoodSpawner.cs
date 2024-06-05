using System;
using Core;
using UI.Core;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _PROJECT.Scripts.InGame.Food
{
    public class FoodSpawner : MonoBehaviour
    {
        [Inject] private Timer _timer;
        [SerializeField] private ComponentPoolFactory _componentPoolFactory;
        [SerializeField] private FoodSpawnerConfig _foodSpawnerConfig;
        private float _foodSpawned;
        private float _timeToNextSpawn;
        public void Start()
        {
            _componentPoolFactory.Initialize(_foodSpawnerConfig.FoodPrefab.gameObject);

            for (int i = 0; i < _foodSpawnerConfig.MaxUnits; i++)
            {
                SpawnFood();
            }
            _timeToNextSpawn = _foodSpawnerConfig.TimeBetweenSpawn;
            
            _timer.OnTick += OnTick;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _foodSpawnerConfig.AreaRadius);
        }

        private void OnTick()
        {
            if (_foodSpawned < _foodSpawnerConfig.MaxUnits)
            {
                _timeToNextSpawn -= _timer.DeltaTime;
                if (_timeToNextSpawn <= 0)
                {
                    SpawnFood();
                    _timeToNextSpawn = _foodSpawnerConfig.TimeBetweenSpawn;
                }
            }
        }

        private void SpawnFood()
        {
            PeopleFood peopleFood = _componentPoolFactory.Get<PeopleFood>();
            peopleFood.Initialize(_foodSpawnerConfig.AreaRadius, transform.position);
            Vector3 appearPos = Random.insideUnitSphere * _foodSpawnerConfig.AreaRadius + transform.position;
            appearPos.y = 0;
            peopleFood.transform.position = appearPos;
            _foodSpawned++;
            peopleFood.OnConsume += OnDeath;
        }

        private void OnDeath(PeopleFood peopleFood)
        {
            peopleFood.OnConsume -= OnDeath;
            peopleFood.Dispose();
            _componentPoolFactory.Release(peopleFood);
            _foodSpawned--;
            
        }

        private void OnDestroy()
        {
            _componentPoolFactory.Dispose();
            _timer.OnTick -= OnTick;
        }
    }
}