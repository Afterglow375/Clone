using UnityEngine;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        private GameObject _enemy;
        private float _enemyHeight;
        private Weapon _weapon;
        private float _elapsedTime;
        private Vector2 _enemyDirection;
        private readonly Vector3 _faceRightScale = new Vector3(1, 1, 1);
        private readonly Vector3 _faceLeftScale = new Vector3(1, -1, 1);

        void Start()
        {
            _enemy = GameObject.Find("Dinosaur");
            _enemyHeight = _enemy.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            _weapon = Instantiate(_weaponPrefab, transform);
        }
        
        void Update()
        {
            if (_weapon.isAttacking)
                return;
            
            Vector2 enemyPosition = GetClosestEnemy();
            AimAtEnemy(enemyPosition);
            
            // if the enemy is in range and weapon isn't on cooldown
            if (Vector2.Distance(enemyPosition, transform.position) <= _weapon.attackRange && 
                _elapsedTime >= 1 / _weapon.attackSpeed)
            {
                _weapon.Attack();
                _elapsedTime = 0;
            }

            _elapsedTime += Time.deltaTime;
        }

        private Vector2 GetClosestEnemy()
        {
            var enemyPosition = _enemy.transform.position;
            return new Vector2(enemyPosition.x, enemyPosition.y + _enemyHeight / 2);
        }
        
        private void AimAtEnemy(Vector3 enemyPosition)
        {
            _enemyDirection = (enemyPosition - transform.position).normalized;
            float angle = Mathf.Atan2(_enemyDirection.y, _enemyDirection.x) * Mathf.Rad2Deg;
            // Make the gun rotate about the character
            Vector3 offset = _enemyDirection * _weapon.pivotDistance;
            // Pivot is on feet, so move the gun up a bit
            offset.y += 0.75f;
            transform.position = transform.parent.position + offset;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            transform.localScale = FacingLeft() ? _faceLeftScale : _faceRightScale;
        }

        private bool FacingLeft()
        {
            return _enemyDirection.x < 0;
        }
    }
}