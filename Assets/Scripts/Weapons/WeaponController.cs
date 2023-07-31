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
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        void Start()
        {
            _weapon = Instantiate(_weaponPrefab, transform);
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;
        }

        void Update()
        {
            if (_weapon.isAttacking)
                return;
            
            Collider2D enemy = UnitManager.Instance.GetClosestEnemy();
            if (enemy == null)
            {
                ResetWeaponPosition();
            }
            else
            {
                Vector3 enemyPosition = enemy.transform.position;
                AimAtEnemy(enemyPosition);
            
                // if the enemy is in attack range and the weapon isn't on cooldown
                if (CanAttackEnemy(enemyPosition))
                {
                    _weapon.Attack();
                    _elapsedTime = 0;
                }
            }
            
            _elapsedTime += Time.deltaTime;
        }

        private void ResetWeaponPosition()
        {
            transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);

            if (UnitManager.Instance.IsPlayerFacingLeft())
            {
                transform.RotateAround(UnitManager.Instance.GetPlayerCenter(), transform.up, 180f);
                transform.localScale = _faceRightScale;
            }
            else
            {
                transform.localScale = _faceRightScale;
            }
        }

        private void AimAtEnemy(Vector3 enemyPosition)
        {
            _enemyDirection = (enemyPosition - UnitManager.Instance.GetPlayerCenter()).normalized;
            float angle = Mathf.Atan2(_enemyDirection.y, _enemyDirection.x) * Mathf.Rad2Deg;
            transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);
            // Make the weapon rotate about the character
            transform.RotateAround(UnitManager.Instance.GetPlayerCenter(), transform.forward, angle);
            transform.localScale = FacingLeft() ? _faceLeftScale : _faceRightScale;
        }

        private bool CanAttackEnemy(Vector3 enemyPosition)
        {
            return Vector2.Distance(enemyPosition, UnitManager.Instance.GetPlayerCenter()) <= _weapon.attackRange &&
                   _elapsedTime >= 1 / _weapon.attackSpeed;
        }

        private bool FacingLeft()
        {
            return _enemyDirection.x < 0;
        }
    }
}