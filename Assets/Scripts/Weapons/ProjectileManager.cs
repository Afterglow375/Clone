using Units;
using UnityEngine;
using UnityEngine.Pool;

namespace Weapons
{
    public class ProjectileManager : MonoBehaviour
    {
        [SerializeField] private int _poolCapacity;
        [SerializeField] private int _poolLimit;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private RangedWeapon _gun;
        private ObjectPool<Projectile> _projectilePool;
        private Player _player;

        private void Start()
        {
            _projectilePool = new ObjectPool<Projectile>(
                CreateFunc,
                ActionOnGet,
                ActionOnRelease,
                ActionOnDestroy,
                false,
                _poolCapacity,
                _poolLimit
            );
            
            _player = GetComponentInParent<Player>();
        }

        public void Shoot()
        {
            Projectile projectile = _projectilePool.Get();
            projectile.transform.position = transform.parent.position;
            projectile.transform.rotation = transform.parent.rotation;
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = transform.parent.right * _gun.projectileVelocity;
            projectile.SetKnockback(projectileRb.velocity.normalized * _gun.knockback);
        }

        public void Despawn(Projectile projectile)
        {
            _projectilePool.Release(projectile);
        }

        private Projectile CreateFunc()
        {
            Projectile projectile = Instantiate(_projectilePrefab);
            projectile.SetAttackRange(_gun.attackRange);
            projectile.SetAttackDamage(_gun.attackDamage);
            projectile.SetProjectileVelocity(_gun.projectileVelocity);
            projectile.SetProjectileManager(this);
            float muzzleDistanceFromCenter =
                Vector2.Distance(transform.parent.position, _player.GetCenter());
            projectile.SetTimeLimit((_gun.attackRange - muzzleDistanceFromCenter) / _gun.projectileVelocity);
            return projectile;
        }
    
        private void ActionOnGet(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }
    
        private void ActionOnRelease(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void ActionOnDestroy(Projectile projectile)
        {
            Destroy(projectile);
        }
    }
}
