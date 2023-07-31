using System;
using Units;
using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private Animator _animator;
        private static readonly int AttackAnim = Animator.StringToHash("Attack");

        public Transform circleOrigin;
        public float radius;

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
            LayerMask mask = LayerMask.GetMask("EnemyHitbox");
            foreach (Collider2D col in Physics2D.OverlapCircleAll(circleOrigin.position, radius, mask))
            {
                if (col.CompareTag("Enemy"))
                {
                    Debug.Log("enemy hit");
                    Enemy enemy = col.transform.parent.GetComponent<Enemy>();
                    enemy.TakeDamage(attackDamage);
                }
            }
        }
    }
}