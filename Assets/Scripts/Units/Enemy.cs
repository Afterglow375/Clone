using System.Runtime.CompilerServices;
using UnityEngine;

namespace Units
{
    public class Enemy : Unit
    {
        public float dmg;
        
        private void Update()
        {
            Player nearestPlayer = UnitManager.Instance.GetNearestPlayer(GetCenter());
            if (nearestPlayer)
            {
                _movement = (nearestPlayer.GetCenter() - (Vector2) transform.position).normalized;
                _animator.SetFloat(Horizontal, _movement.x);
                _animator.SetFloat(Speed, _moveSpeed);

                _spriteRenderer.flipX = IsFacingLeft();
            }
        }

        private void FixedUpdate()
        {
            if (!IsKnocked())
            {
                _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
