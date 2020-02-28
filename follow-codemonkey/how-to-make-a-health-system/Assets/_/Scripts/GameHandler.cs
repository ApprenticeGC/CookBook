namespace GiantCroissant.FollowCodeMonkey.HowToMakeAHealthSystem
{
    using System;
    using UnityEngine;
    using UnityEngine.Serialization;

    public class GameHandler : MonoBehaviour
    {
        public UnityEngine.UI.Button damageButton;
        public UnityEngine.UI.Button healButton;

        public UnityEngine.UI.Slider healthSlider;

        public HealthBar healthBar;

        public GameObject healthBarPrefab;
        
        void Start()
        {
            var healthSystem = new HealthSystem(100, 100);

            var healthBarGo = Instantiate(healthBarPrefab);
            var healthBarComp = healthBarGo.GetComponent<HealthBar>();
            
            healthBar.Setup(healthSystem);
            if (healthBarComp != null)
            {
                healthBarComp.Setup(healthSystem);
            }

            healthSlider.value = healthSystem.HealthPercentage;
            Debug.Log($"Health: {healthSystem.Health}");

            healthSystem.Damage(90);
            healthSlider.value = healthSystem.HealthPercentage;
            Debug.Log($"Health: {healthSystem.Health}");
            
            healthSystem.Heal(80);
            healthSlider.value = healthSystem.HealthPercentage;
            Debug.Log($"Health: {healthSystem.Health}");

            damageButton.onClick.AddListener(() =>
            {
                healthSystem.Damage(10);
                healthSlider.value = healthSystem.HealthPercentage;
                Debug.Log($"Health: {healthSystem.Health}");
            });

            healButton.onClick.AddListener(() =>
            {
                healthSystem.Heal(10);
                healthSlider.value = healthSystem.HealthPercentage;
                Debug.Log($"Health: {healthSystem.Health}");
            });
        }

        private void OnDestroy()
        {
            damageButton.onClick.RemoveAllListeners();
            healButton.onClick.RemoveAllListeners();
            
        }
    }
}
