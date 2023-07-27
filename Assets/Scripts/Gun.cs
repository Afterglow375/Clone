using UnityEditor.Search;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private GameObject _bulletPrefab;
    public float bulletForce;
    private GameObject _enemy;
    private float _enemyHeight;
    private Vector3 _enemyPosition;
    private Transform _muzzle;
    private float _time;
    
    void Start()
    {
        _muzzle = transform.GetChild(0).transform;
        _enemy = GameObject.Find("Dinosaur");
        _enemyHeight = _enemy.GetComponent<SpriteRenderer>().bounds.size.y/2;
        _time = _attackSpeed;
    }
    
    void Update()
    {
        _enemyPosition = GetClosestEnemy();
        Vector2 direction = (_enemyPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        _spriteRenderer.flipY = direction.x < 0;

        _time += Time.deltaTime;
        if (_time >= _attackSpeed)
        {
            Shoot();
            _time = 0;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = _muzzle.right * bulletForce;
    }

    private Vector2 GetClosestEnemy()
    {
        var enemyPosition = _enemy.transform.position;
        return new Vector2(enemyPosition.x, enemyPosition.y + _enemyHeight / 2);
    }
}
