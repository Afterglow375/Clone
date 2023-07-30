using System;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public float attackSpeed;
        public float attackRange;
        public float attackDamage;
        public float pivotDistance;
        public float knockback;
        
        public abstract void Attack();
    }
}