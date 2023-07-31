using UnityEngine;

namespace Utils
{
    public class LayerMaskHelper 
    {
        public static LayerMask EnemyMask = LayerMask.GetMask("Enemy");
        public static LayerMask EnemyHitboxMask = LayerMask.GetMask("EnemyHitbox");
    }
}