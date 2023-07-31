using UnityEngine;

namespace Units
{
    public class Player : Unit
    {
        void Update()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.y = Input.GetAxisRaw("Vertical");

            _animator.SetFloat(Horizontal, _movement.x);
            _animator.SetFloat(Speed, _movement.sqrMagnitude);

            _spriteRenderer.flipX = _movement.x < 0;
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        }

        public override void Die()
        {
            _currHp = _maxHp;
        }
    }
}
