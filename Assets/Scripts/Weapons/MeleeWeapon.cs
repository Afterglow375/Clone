using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private Animator _animator;
        private static readonly int AttackAnim = Animator.StringToHash("Attack");

        public override void Attack()
        {
            _animator.SetTrigger(AttackAnim);
        }
    }
}