using System;
using UnityEngine;

namespace Units
{
    public class Player : Unit
    {
        // params: player hp after taking dmg, enemy damage
        public static event Action<float, float> PlayerHealthChangeEvent;
        
        void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _animator.SetFloat(Horizontal, _movement.x);
            _animator.SetFloat(Speed, _movement.sqrMagnitude);

            _spriteRenderer.flipX = IsFacingLeft();
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement.normalized * _moveSpeed * Time.fixedDeltaTime);
        }

        public override void TakeDamage(float dmg)
        {
            _currHp -= dmg;
            Debug.Log(_currHp);
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
