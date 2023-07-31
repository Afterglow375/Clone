using UnityEngine;

namespace Units
{
    public class Enemy : Unit
    {
        private void Update()
        {
            _movement = (UnitManager.Instance.GetPlayerPivot() - transform.position).normalized;
            _animator.SetFloat(Horizontal, _movement.x);
            _animator.SetFloat(Speed, _moveSpeed);

            _spriteRenderer.flipX = _movement.x > 0;
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
