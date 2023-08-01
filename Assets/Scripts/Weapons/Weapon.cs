using System;
using Units;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public float attackAnimationLength;
        public float attackSpeed;
        public float attackRange;
        public float attackDamage;
        public float knockback;
        public bool isAttacking;

        protected Player _player;

        public void Start()
        {
            _player = GetComponentInParent<Player>();
        }

        protected virtual void StartImpl() {}

        public void Attack()
        {
            isAttacking = true;
            AttackImpl();
        }

        protected abstract void AttackImpl();

        // used by animation event when the attack ends
        public void ResetIsAttacking()
        {
            isAttacking = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;;
            Gizmos.DrawWireSphere(_player.GetCenter(), attackRange);
        }
    }
}