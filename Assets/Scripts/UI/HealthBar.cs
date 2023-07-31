using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;
        private float _maxHealth;
        
        private void Awake()
        {
            Player.PlayerHealthChangeEvent += UpdateHealthBar;
        }

        private void Start()
        {
            _maxHealth = UnitManager.Instance.GetPlayerMaxHp();
        }

        private void OnDestroy()
        {
            Player.PlayerHealthChangeEvent -= UpdateHealthBar;
        }
        
        public void UpdateHealthBar(float currentHealth, float damage)
        {
            _healthbarSprite.fillAmount = currentHealth / _maxHealth;
        }
    }
}
