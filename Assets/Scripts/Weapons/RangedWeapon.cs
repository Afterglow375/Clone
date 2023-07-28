using UnityEngine;

namespace Weapons
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ProjectileManager _projectileManager;
        public float projectileVelocity;
        private GameObject _enemy;
        private float _enemyHeight;
        private Vector3 _enemyPosition;
        private float _elapsedTime;
    
        void Start()
        {
            _enemy = GameObject.Find("Dinosaur");
            _enemyHeight = _enemy.GetComponent<SpriteRenderer>().bounds.size.y/2;
            _elapsedTime = attackSpeed;
        }
    
        void Update()
        {
            _enemyPosition = GetClosestEnemy();
            PointAtEnemy();
        
            // if we're out of range don't attack
            if (Vector2.Distance(_enemyPosition, transform.position) <= attackRange)
            {
                ShootAtEnemy();
            }

            _elapsedTime += Time.deltaTime;
        }

        private void PointAtEnemy()
        {
            Vector2 direction = (_enemyPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // Make the gun rotate about the character
            Vector3 offset = direction * pivotDistance;
            // Pivot is on feet, so move the gun up a bit
            offset.y += 0.75f;
            transform.position = transform.parent.position + offset;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            _spriteRenderer.flipY = direction.x < 0;
        }

        private void ShootAtEnemy()
        {
            if (_elapsedTime >= 1 / attackSpeed)
            {
                _projectileManager.Shoot();
                _elapsedTime = 0;
            }
        }

        private Vector2 GetClosestEnemy()
        {
            var enemyPosition = _enemy.transform.position;
            return new Vector2(enemyPosition.x, enemyPosition.y + _enemyHeight / 2);
        }
    }
}
