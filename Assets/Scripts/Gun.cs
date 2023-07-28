using UnityEditor.Search;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ProjectileManager _projectileManager;
    public float attackSpeed;
    public float attackRange;
    public float attackDamage;
    public float projectileVelocity;
    private GameObject _enemy;
    private float _enemyHeight;
    private Vector3 _enemyPosition;
    private float _elapsedTime;
    
    void Start()
    {
        _enemy = GameObject.Find("Dinosaur");
        _enemyHeight = _enemy.GetComponent<SpriteRenderer>().bounds.size.y/2;
        _elapsedTime = attackSpeed;
    }
    
    void Update()
    {
        _enemyPosition = GetClosestEnemy();
        
        // if we're out of range don't attack
        if (Vector2.Distance(_enemyPosition, transform.position) > attackRange)
        {
            transform.rotation = Quaternion.identity;
            _spriteRenderer.flipY = false;
        }
        else
        {
            ShootAtEnemy();
        }

        _elapsedTime += Time.deltaTime;
    }

    private void ShootAtEnemy()
    {
        Vector2 direction = (_enemyPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        _spriteRenderer.flipY = direction.x < 0;
        if (_elapsedTime >= 1 / attackSpeed)
        {
            _projectileManager.Shoot();
            _elapsedTime = 0;
        }
    }

    private Vector2 GetClosestEnemy()
    {
        var enemyPosition = _enemy.transform.position;
        return new Vector2(enemyPosition.x, enemyPosition.y + _enemyHeight / 2);
    }
}
