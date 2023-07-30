using UnityEngine;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        private Enemy _enemy;
        private PlayerController _player;
        private float _enemyHeight;
        private Weapon _weapon;
        private float _elapsedTime;
        private Vector2 _enemyDirection;
        private readonly Vector3 _faceRightScale = new Vector3(1, 1, 1);
        private readonly Vector3 _faceLeftScale = new Vector3(1, -1, 1);

        void Start()
        {
            _enemy = GameObject.Find("Dinosaur").GetComponent<Enemy>();
            _player = transform.parent.GetComponent<PlayerController>();
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
            
            if (Vector2.Distance(enemyPosition, _player.GetCenter()) <= _weapon.attackRange && 
                _elapsedTime >= 1 / _weapon.attackSpeed)
            {
                _weapon.Attack();
                _elapsedTime = 0;
            }

            _elapsedTime += Time.deltaTime;
        }
        
        private Vector2 GetClosestEnemy()
        {
            return _enemy.GetCenter();
        }
        
        private void AimAtEnemy(Vector3 enemyPosition)
        {
            _enemyDirection = (enemyPosition - _player.GetCenter()).normalized;
            float angle = Mathf.Atan2(_enemyDirection.y, _enemyDirection.x) * Mathf.Rad2Deg;
            // Make the gun rotate about the character
            Vector3 offset = _enemyDirection * _weapon.pivotDistance;
            transform.position = _player.GetCenter() + offset;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            transform.localScale = FacingLeft() ? _faceLeftScale : _faceRightScale;
        }

        private bool FacingLeft()
        {
            return _enemyDirection.x < 0;
        }
    }
}