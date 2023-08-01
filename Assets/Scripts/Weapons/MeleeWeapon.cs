using System;
using Units;
using UnityEngine;
using Utils;

namespace Weapons
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private Animator _animator;
        private static readonly int AttackAnim = Animator.StringToHash("Attack");

        public Transform circleOrigin;
        public float radius;

        private void Awake()
        {
            // speed up animation to keep up with attack speed if necessary
            _animator.speed = Mathf.Max(attackSpeed * attackAnimationLength, 1f);
            attackAnimationLength /= _animator.speed;
        }

        protected override void AttackImpl()
        {
            _animator.SetTrigger(AttackAnim);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
            Gizmos.DrawWireSphere(position, radius);
        }

        // Called by attack animation
        public void DetectColliders()
        {
            foreach (Collider2D col in Physics2D.OverlapCircleAll(circleOrigin.position, radius, LayerMaskHelper.EnemyHitboxMask))
            {
                Enemy enemy = col.transform.parent.GetComponent<Enemy>();
                Vector2 knockbackVector = (col.transform.position - _player.GetCenter()).normalized * knockback;
                enemy.TakeDamage(attackDamage, knockbackVector);
            }
        }
    }
}