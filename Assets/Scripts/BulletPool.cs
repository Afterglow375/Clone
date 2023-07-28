using System;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static ObjectPool<Bullet> Pool;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolLimit;
    [SerializeField] private Bullet _bulletPrefab;

    private void Start()
    {
        Pool = new ObjectPool<Bullet>(
            CreateFunc,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy,
            false,
            _poolCapacity,
            _poolLimit
        );
    }

    private Bullet CreateFunc()
    {
        return Instantiate(_bulletPrefab);
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet);
    }
}
