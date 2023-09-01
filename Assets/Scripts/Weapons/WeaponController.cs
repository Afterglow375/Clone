using Units;
using Unity.Netcode;
using UnityEngine;

namespace Weapons
{
    public class WeaponController : NetworkBehaviour
    {
        [SerializeField] private Weapon _weaponPrefab;
        private Transform _weaponRotator;
        private Weapon _weapon;
        private Player _player;
        private float _weaponCooldownTimer;
        private float _timeBetweenAttacks;
        private Vector2 _enemyDirection;
        private readonly Vector3 _faceRightScale = new Vector3(1, 1, 1);
        private readonly Vector3 _faceLeftScale = new Vector3(1, -1, 1);
        private Vector3 _faceLeftPosition;
        private Vector3 _originalPosition;
        private Quaternion _originalRotation;
        
        public override void OnNetworkSpawn()
        {
            // _weapon = Instantiate(_weaponPrefab, transform);
            _weapon = GetComponentInChildren<Weapon>();
            if (IsOwner)
            {
                // _weapon.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
                // NetworkObject.TrySetParent(transform);
                _weaponRotator = transform.parent;
                _timeBetweenAttacks = 1 / _weapon.attackSpeed - _weapon.attackAnimationLength;
                _originalPosition = transform.localPosition;
                _faceLeftPosition = _originalPosition;
                _faceLeftPosition.y *= -1;
                _originalRotation = transform.rotation;
                _player = GetComponentInParent<Player>();
            }
        }

        void Update()
        {
            if (!IsOwner || _weapon.isAttacking)
                return;
            
            Collider2D enemy = UnitManager.Instance.GetNearestEnemy(_player.GetCenter());
            if (enemy == null)
            {
                ResetWeaponPosition();
            }
            else
            {
                Vector2 enemyPosition = enemy.transform.position;
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
            // Debug.Log(_originalRotation);
            // Debug.Log(_originalPosition);
            transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);

            if (_player.IsFacingLeft())
            {
                transform.RotateAround(_player.GetCenter(), transform.up, 180f);
                transform.localScale = _faceLeftScale;
            }
            else
            {
                transform.localScale = _faceRightScale;
            }
        }

        private void AimAtEnemy(Vector2 enemyPosition)
        {
            // _enemyDirection = (enemyPosition - _player.GetCenter()).normalized;
            // float angle = Mathf.Atan2(_enemyDirection.y, _enemyDirection.x) * Mathf.Rad2Deg;
            // transform.SetLocalPositionAndRotation(_originalPosition, _originalRotation);
            // // Make the weapon rotate about the character
            // transform.RotateAround(_player.GetCenter(), transform.forward, angle);
            _enemyDirection = enemyPosition - _player.GetCenter();
            _weaponRotator.right = _enemyDirection;
            transform.localScale = FacingLeft() ? _faceLeftScale : _faceRightScale;
            transform.localPosition = FacingLeft() ? _faceLeftPosition : _originalPosition;
        }

        private bool CanAttackEnemy(Vector2 enemyPosition)
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