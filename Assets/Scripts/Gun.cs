using UnityEditor.Search;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private GameObject _bulletPrefab;
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
        var enemyPosition = _enemy.transform.position;
        _enemyPosition = new Vector3(enemyPosition.x, enemyPosition.y + _enemyHeight / 2);
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
        Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
    }
}
