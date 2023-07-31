using System.Linq;
using Units;
using UnityEngine;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        private Weapon _weapon;
        private float _elapsedTime;
        private Vector2 _enemyDirection;
        private readonly Vector3 _faceRightScale = new Vector3(1, 1, 1);
        private readonly Vector3 _faceLeftScale = new Vector3(1, -1, 1);

        void Start()
        {
            _weapon = Instantiate(_weaponPrefab, transform);
        }
        
        void Update()
        {
            if (_weapon.isAttacking)
                return;
            
            Vector3 enemyPosition = UnitManager.Instance.GetClosestEnemyPosition();
            AimAtEnemy(enemyPosition);
            
            // if the enemy is in range and weapon isn't on cooldown
            if (Vector2.Distance(enemyPosition, UnitManager.Instance.GetPlayerCenter()) <= _weapon.attackRange && 
                _elapsedTime >= 1 / _weapon.attackSpeed)
            {
                _weapon.Attack();
                _elapsedTime = 0;
            }

            _elapsedTime += Time.deltaTime;
        }
        
        private void AimAtEnemy(Vector3 enemyPosition)
        {
            _enemyDirection = (enemyPosition - UnitManager.Instance.GetPlayerCenter()).normalized;
            float angle = Mathf.Atan2(_enemyDirection.y, _enemyDirection.x) * Mathf.Rad2Deg;
            // Make the gun rotate about the character
            Vector3 offset = _enemyDirection * _weapon.pivotDistance;
            transform.position = UnitManager.Instance.GetPlayerCenter() + offset;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            transform.localScale = FacingLeft() ? _faceLeftScale : _faceRightScale;
        }

        private bool FacingLeft()
        {
            return _enemyDirection.x < 0;
        }
    }
}