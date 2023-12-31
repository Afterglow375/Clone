using System;
using Managers;
using UnityEngine;

namespace Units
{
    public class Player : Unit
    {
        public float dashDuration;
        public float dashCooldown;
        public float dashSpeedMultiplier;
        private float _dashCooldownTimer;
        private float _dashSpeed;
        private float _dashTimer;
        private bool _isDashing; 
        
        // params: player hp after taking dmg, enemy damage
        public static event Action<float, float> PlayerHealthChangeEvent;

        protected override void StartImpl()
        {
            _dashSpeed = _moveSpeed * dashSpeedMultiplier;
        }

        void Update()
        {
            if (GameManager.Instance.IsPaused)
                return;
            
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _animator.SetFloat(Horizontal, _movement.x);
            _animator.SetFloat(Speed, _movement.sqrMagnitude);

            _spriteRenderer.flipX = IsFacingLeft();
            
            _dashCooldownTimer -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && IsMoving() && _dashCooldownTimer <= 0)
            {
                StartDash();
                _dashCooldownTimer = dashCooldown;
            }
        }

        private void FixedUpdate()
        {
            if (_isDashing)
            {
                _dashTimer -= Time.fixedDeltaTime;
                if (_dashTimer <= 0f)
                {
                    _rb.velocity = Vector2.zero;
                    _isDashing = false;
                }
            }
            else
            {
                _rb.MovePosition(_rb.position + _movement.normalized * _moveSpeed * Time.fixedDeltaTime);
            }
        }

        private bool IsMoving()
        {
            return _movement != Vector2.zero;
        }
        
        private void StartDash()
        {
            _isDashing = true;
            _dashTimer = dashDuration;
            _rb.velocity = _movement.normalized * _dashSpeed;
        }

        public override void TakeDamage(float dmg)
        {
            _currHp -= dmg;
            PlayerHealthChangeEvent?.Invoke(_currHp, dmg);
            if (_currHp <= 0)
            {
                Die();
            }
        }

        protected override void Die()
        {
            _currHp = _maxHp;
        }

        public float GetMaxHp()
        {
            return _maxHp;
        }
    }
}
