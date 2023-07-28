using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Enemy")
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        
        BulletPool.Pool.Release(this);
    }
}
