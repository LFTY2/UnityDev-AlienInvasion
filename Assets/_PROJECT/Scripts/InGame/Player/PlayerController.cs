using System;
using System.Collections.Generic;
using System.Linq;
using _PROJECT.Scripts.InGame.Bank;
using _PROJECT.Scripts.InGame.Food;
using _PROJECT.Scripts.InGame.Player;
using Core;
using DG.Tweening;
using UI.Core;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace _PROJECT.Scripts
{
    public class PlayerController : MonoBehaviour, IDamageable, IDisposable, IRadius
    {
        public event Action OnDeath;
        public event Action<int, int> OnHealthChange;
        public event Action<int, int> OnFoodChange; // <max, current>
        public event Action<float> OnRadiusChange;

        [Inject] private FoodVault _foodVault;
        [Inject] private Timer _timer;
        [Inject] private InGameHandlerBase _inGameHandler;
        [Inject] private AudioHandler _audioHandler;

        [SerializeField] private LayerMask _foodLayer;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerStatsConfig _playerStatsConfig;
        [SerializeField] private ComponentPoolFactory _componentPoolFactory;
        [SerializeField] private LineDamage _lineDamagePrefab;
        [SerializeField] private RadiusDisplay _radiusDisplay;
        
        private int _food;
        private int _maxFood;
        private int _maxHealth;
        private int _health;
        private int _damage;
        private float _hitRadius;
        private int _hitTargets;
        private Transform _transform;
        private Dictionary<IDamageable, LineDamage> _damageable = new Dictionary<IDamageable, LineDamage>();
        
        public Transform Transform => _transform;
        public int Health => _health;
        public float Radius => _hitRadius;
        public void Initialize()
        {
            _playerMovement.Initialize();
            _transform = transform;
            _damage = _playerStatsConfig.Damage;
            _maxHealth = _playerStatsConfig.Health;
            _hitRadius = _playerStatsConfig.HitRadius;
            _hitTargets = _playerStatsConfig.TargetHitMax;
            _maxFood = _playerStatsConfig.MaxFood;
            _health = _maxHealth;
            _radiusDisplay.Initialize(this);
            
            InvokeHealthChange();
            InvokeRadiusChange();
            InvokeFoodChange();

            _timer.OnSecondTick += OnSecondTick;
            _timer.OnTick += FindEnemy;
            _componentPoolFactory.Initialize(_lineDamagePrefab.gameObject);
        }
        
        private void FindEnemy()
        {
            if (_food >= _maxFood) 
            {
                if (_damageable.Count > 0)
                {
                    _damageable.Clear();
                }
                return;
            }
            foreach (var damageable in _damageable.Keys.ToList())
            {
                float distance = (damageable.Transform.position - _transform.position).magnitude;
                if (distance > _hitRadius)
                {
                    _damageable[damageable].Dispose();
                    _componentPoolFactory.Release(_damageable[damageable]);
                    _damageable.Remove(damageable);
                }
            }
            
            if (_damageable.Count >= _hitTargets) return;
           
            Collider[] colliders = Physics.OverlapSphere(_transform.position, _hitRadius, _foodLayer);
            foreach (var collide in colliders)
            {
                IDamageable damageable = collide.GetComponent<IDamageable>();
                if (damageable != null && damageable.Health > 0 && !_damageable.ContainsKey(damageable))
                {
                    LineDamage lineDamage = _componentPoolFactory.Get<LineDamage>();
                    lineDamage.Initialize(damageable.Transform);
                    _damageable.Add(damageable, lineDamage);
                    if (_damageable.Count >= _hitTargets) break;
                }
            }
        }
        
        private void OnSecondTick()
        {
            foreach (var damageable in _damageable.Keys.ToList())
            {
                damageable.TakeDamage(_damage);
                if (damageable.Health <= 0)
                {
                    PeopleFood peopleFood = damageable.Transform.GetComponent<PeopleFood>();
                    _damageable[peopleFood].Dispose();
                    _componentPoolFactory.Release(_damageable[peopleFood]);
                    _damageable.Remove(peopleFood);
                    if (peopleFood != null)
                    {
                        peopleFood.Consumed(_transform);
                        peopleFood.OnConsume += FoodConsumed;
                    }
                }
            }
        }

        private void FoodConsumed(PeopleFood peopleFood)
        {
            peopleFood.OnConsume -= FoodConsumed;
            _food = Math.Min(_food + peopleFood.FoodNum, _maxFood);
            InvokeFoodChange();
        }
        
        public void Heal(int heal)
        {
            _health += heal;
            _audioHandler.PlaySound(AudioClipType.Heal);
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            InvokeHealthChange();
        }
        
        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                _health = 0;
                Death();
            }
            InvokeHealthChange();
        }
        public void StoreFood()
        {
            _foodVault.Food += _food;
            _food = 0;
            InvokeFoodChange();
        }


      

        private void InvokeHealthChange()
        {
            OnHealthChange?.Invoke(_maxHealth, _health);
        }

        private void InvokeRadiusChange()
        {
            OnRadiusChange?.Invoke(_hitRadius);
        }

        private void InvokeFoodChange()
        {
            OnFoodChange?.Invoke(_maxFood, _food);
        }

        private void Death()
        {
            OnDeath?.Invoke();
        }
        
        public void Dispose()
        {
            _timer.OnSecondTick -= OnSecondTick;
            _timer.OnTick -= FindEnemy;
            _componentPoolFactory.Dispose();
            _radiusDisplay.Dispose();
        }
    }
}