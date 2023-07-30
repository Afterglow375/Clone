using UnityEngine;

namespace Weapons
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private Animator _animator;
        private static readonly int AttackAnim = Animator.StringToHash("Attack");

        protected override void AttackImpl()
        {
            _animator.SetTrigger(AttackAnim);
        }
    }
}