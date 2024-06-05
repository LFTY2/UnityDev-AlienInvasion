using System;
using UnityEngine;

namespace _PROJECT.Scripts
{
    public interface IDamageable
    {
        public event Action<int, int> OnHealthChange; // <max, current> 
        
        public void TakeDamage(int damage);
        public void Heal(int heal);
        public int Health { get; }
        public Transform Transform { get; }
    }
}