using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public float hp = 100f;
    private Transform _player;
    private Vector2 _movement;
    private static readonly int Horizontal = Animator.StringToHash("horizontal");
    private static readonly int Speed = Animator.StringToHash("speed");

    void Start()
    {
        _player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        _movement = (_player.position - transform.position).normalized;
        _animator.SetFloat(Horizontal, _movement.x);
        _animator.SetFloat(Speed, _moveSpeed);

        _spriteRenderer.flipX = _movement.x > 0;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
    
    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
