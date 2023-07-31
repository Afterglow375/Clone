using System;
using UnityEngine;
using Utils;

namespace Units
{
    public class EnemyHitbox : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                float enemyDmg = _enemy.dmg;
                UnitManager.Instance.TakePlayerDamage(enemyDmg);
            }
        }
    }
}
