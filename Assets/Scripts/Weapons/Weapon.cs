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
            Gizmos.DrawWireSphere(UnitManager.Instance.GetPlayerCenter(), attackRange);
        }
    }
}