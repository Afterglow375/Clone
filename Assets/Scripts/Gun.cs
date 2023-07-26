using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Transform _enemy;

    void Start()
    {
        _enemy = GameObject.Find("Dinosaur").transform;
    }
    
    void Update()
    {
        Vector2 direction = (_enemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        _spriteRenderer.flipY = direction.x < 0;
    }
}
