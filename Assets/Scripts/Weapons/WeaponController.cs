using Units;
using UnityEngine;

namespace Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        private Weapon _weapon;
        private Player _player;
        private float _weaponCooldownTimer;
        private float _timeBetweenAttacks;
        private Vector2 _enemyDirection;
        private readonly Vector3 _faceRightScale = new Vector3(1, 1, 1);
        private readonly Vector3 _faceLeftScale = new Vector3(1, -1, 1);
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        void Start()
        {
            _weapon = Instantiate(_weaponPrefab, transform);
            _timeBetweenAttacks = 1 / _weapon.attackSpeed - _weapon.attackAnimationLength;
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;
            _player = GetComponentInParent<Player>();
        }

        void Update()
        {
            if (_weapon.isAttacking)
                return;
            
            Collider2D enemy = UnitManager.Instance.GetNearestEnemy(_player.GetCenter());
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
                    _weaponCooldownTimer = 0;
                }
            }
            
            _weaponCooldownTimer += Time.deltaTime;
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
            _enemyDirection = (enemyPosition - _player.GetCenter()).normalized;
            float angle = Mathf.Atan2(_enemyDirection.y, _enemyDirection.x) * Mathf.Rad2Deg;
            transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);
            // Make the weapon rotate about the character
            transform.RotateAround(_player.GetCenter(), transform.forward, angle);
            transform.localScale = FacingLeft() ? _faceLeftScale : _faceRightScale;
        }

        private bool CanAttackEnemy(Vector3 enemyPosition)
        {
            return Vector2.Distance(enemyPosition, _player.GetCenter()) <= _weapon.attackRange &&
                   _weaponCooldownTimer >= _timeBetweenAttacks;
        }

        private bool FacingLeft()
        {
            return _enemyDirection.x < 0;
        }
    }
}