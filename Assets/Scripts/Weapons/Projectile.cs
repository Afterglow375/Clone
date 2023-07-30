using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        private float _attackRange;
        private float _attackDamage;
        private float _projectileVelocity;
        private float _timeElapsed;
        private float _timeLimit;
        private float _attackLength;
        private float _gun;
        private ProjectileManager _projectileManager; 
    
        public void SetProjectileManager(ProjectileManager projectileManager)
        {
            _projectileManager = projectileManager;
        }

        public void SetAttackRange(float attackRange)
        {
            _attackRange = attackRange;
        }
    
        public void SetAttackDamage(float attackDamage)
        {
            _attackDamage = attackDamage;
        }
    
        public void SetProjectileVelocity(float projectileVelocity)
        {
            _projectileVelocity = projectileVelocity;
        }

        public void SetTimeLimit()
        {
            _timeLimit = _attackRange / _projectileVelocity;
        }

        private void Update()
        {
            _timeElapsed += Time.deltaTime;
            if (_timeElapsed >= _timeLimit)
            {
                _projectileManager.Despawn(this);
                _timeElapsed = 0;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.transform.parent.GetComponent<Enemy>();
                enemy.TakeDamage(_attackDamage);
            }
        
            _projectileManager.Despawn(this);
        }
    }
}
