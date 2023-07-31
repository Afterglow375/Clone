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

            _spriteRenderer.flipX = IsFacingLeft();
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement.normalized * _moveSpeed * Time.fixedDeltaTime);
        }

        protected override void Die()
        {
            _currHp = _maxHp;
        }
    }
}
