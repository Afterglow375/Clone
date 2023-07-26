using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private GameObject _enemy;
    private float _enemyHeight;
    private Vector3 _enemyPosition;

    void Start()
    {
        _enemy = GameObject.Find("Dinosaur");
        _enemyHeight = _enemy.GetComponent<SpriteRenderer>().bounds.size.y/2;
    }
    
    void Update()
    {
        var enemyPosition = _enemy.transform.position;
        _enemyPosition = new Vector3(enemyPosition.x, enemyPosition.y + _enemyHeight / 2);
        Vector2 direction = (_enemyPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        _spriteRenderer.flipY = direction.x < 0;
    }
}
