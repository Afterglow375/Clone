using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthbarSprite;
        private Player _player;
        private float _maxHealth;
        
        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _player.PlayerHealthChangeEvent += UpdateHealthBar;
        }

        private void Start()
        {
            _maxHealth = _player.GetMaxHp();
        }

        private void OnDestroy()
        {
            _player.PlayerHealthChangeEvent -= UpdateHealthBar;
        }
        
        public void UpdateHealthBar(float currentHealth, float damage)
        {
            _healthbarSprite.fillAmount = currentHealth / _maxHealth;
        }
    }
}
