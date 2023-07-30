using UnityEngine;

namespace Weapons
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private ProjectileManager _projectileManager;
        public float projectileVelocity;
        public float numPiercing;
        public float piercingFalloff;
    
        public override void Attack()
        {
            _projectileManager.Shoot();
        }
    }
}
