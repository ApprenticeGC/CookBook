namespace GiantCroissant.FollowCodeMonkey.HowToMakeAHealthSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HealthSystem
    {
        public event System.Action OnHealthChanged;
        
        private int _health;
        private int _maxHealth;

        public int Health => _health;

        public float HealthPercentage => (float)_health / (float)_maxHealth;

        public HealthSystem(int health, int maxHealth)
        {
            _health = health;
            _maxHealth = maxHealth;
        }

        public void Damage(int amount)
        {
            _health -= amount;
            if (_health < 0) _health = 0;
            OnHealthChanged?.Invoke();
        }

        public void Heal(int amount)
        {
            _health += amount;
            if (_health > _maxHealth) _health = _maxHealth;
            OnHealthChanged?.Invoke();
        }
    }
}
