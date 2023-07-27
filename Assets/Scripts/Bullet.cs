using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Enemy")
        {
            Enemy enemy = col.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
