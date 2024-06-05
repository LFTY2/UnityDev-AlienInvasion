using System;
using Core;
using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace _PROJECT.Scripts.InGame.Food
{
    public class PeopleFood : MonoBehaviour, IDamageable, IDisposable
    {
        public event Action<int, int> OnHealthChange;
        public event Action<PeopleFood> OnConsume;
        
        [Inject] private Timer _timer;

        [SerializeField] private Animator _animatorController;
        [SerializeField] private UIHpBar _uiHpBar;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private FoodConfig _foodConfig;
        
        private int _health;
        private int _maxHealth;
        private Transform _transform;
        private float _moveRadius;
        private Vector3 _center;
        private float _timeToChangePos;
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        public int FoodNum => _foodConfig.FoodGive;
        public int Health => _health;
        public Transform Transform => _transform;
        private void Awake()
        {
            _transform = transform;
        }

        public void Initialize(float moveRadius, Vector3 center)
        {
            _center = center;
            _moveRadius = moveRadius;
            _maxHealth = _foodConfig.Health;
            _health = _maxHealth;
            InvokeHealthChange();
            _timer.OnTick += MoveToRandomPos;
            _uiHpBar.Initialize(this);
            _uiHpBar.gameObject.SetActive(false);
            _agent.enabled = true;
        }
        public void TakeDamage(int damage)
        {
            _uiHpBar.gameObject.SetActive(true);
            _health -= damage;
            if (_health <= 0)
            {
                _health = 0;
            }
            InvokeHealthChange();
        }

        private void MoveToRandomPos()
        {
            _timeToChangePos -= _timer.DeltaTime;
            _animatorController.SetBool(IsRun,_agent.velocity.magnitude > 0);
            if (_timeToChangePos <= 0)
            {
                Vector3 randPos = Random.insideUnitSphere * _moveRadius + _center;
                randPos.y = 0;
                _agent.SetDestination(randPos);
                _timeToChangePos = _foodConfig.TimeChangePos;
            }
            
        }
        
        public void Heal(int heal)
        {
            
        }

        public void Consumed(Transform eatTransform)
        {
            _timer.OnTick -= MoveToRandomPos;
            _animatorController.SetBool(IsRun,false);
            _agent.enabled = false;
            transform.parent = eatTransform;
            transform.DOLocalMove(Vector3.zero, 1f).OnComplete(InvokeConsumed);
        }

        private void InvokeConsumed()
        {
            OnConsume?.Invoke(this);
        }
        private void InvokeHealthChange()
        {
            OnHealthChange?.Invoke(_maxHealth, _health);
        }
        
        public void Dispose()
        {
            _timer.OnSecondTick -= MoveToRandomPos;
        }
    }
}