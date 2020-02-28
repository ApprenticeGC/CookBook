namespace GiantCroissant.FollowCodeMonkey.HowToMakeAHealthSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HealthBar : MonoBehaviour
    {
        private HealthSystem _healthSystem;
        
        public void Setup(HealthSystem healthSystem)
        {
            _healthSystem = healthSystem;
            
            _healthSystem.OnHealthChanged += HealthSystemOnOnHealthChanged;
        }

        private void HealthSystemOnOnHealthChanged()
        {
            transform.Find("Bar").localScale = new Vector3(_healthSystem.HealthPercentage, 1, 1);
        }

        private void OnDestroy()
        {
            _healthSystem.OnHealthChanged -= HealthSystemOnOnHealthChanged;
        }

        // void Update()
        // {
        //     transform.Find("Bar").localScale = new Vector3(_healthSystem.HealthPercentage, 1, 1);
        // }
    }
}