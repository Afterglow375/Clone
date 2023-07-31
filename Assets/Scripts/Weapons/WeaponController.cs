using System.Linq;
using Units;
using UnityEngine;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private Player _player;
        private Weapon _weapon;
        private float _elapsedTime;
        private Vector2 _enemyDirection;
        private readonly Vector3 _faceRightScale = new Vector3(1, 1, 1);
        private readonly Vector3 _faceLeftScale = new Vector3(1, -1, 1);
        private Vector3 _originalPosition;
        private Vector3 _originalPositionLeft;
        private Quaternion _originalRotation;
        private Quaternion _originalRotationLeft;

        void Start()
        {
            _weapon = Instantiate(_weaponPrefab, transform);
            _originalPosition = transform.position;
            _originalPositionLeft = new Vector3(-_originalPosition.x, _originalPosition.y, _originalPosition.z);
            _originalRotation = transform.rotation;
            _originalRotationLeft = new Quaternion(_originalRotation.x, _originalRotation.y, -_originalRotation.z, _originalRotation.w);
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

            if (_player.IsFacingLeft())
            {
                transform.RotateAround(_player.GetCenter(), transform.up, 180f);
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
            // Make the weapon rotate about the character
            Vector3 offset = _enemyDirection * _weapon.pivotDistance;
            transform.position = UnitManager.Instance.GetPlayerCenter() + offset;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
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