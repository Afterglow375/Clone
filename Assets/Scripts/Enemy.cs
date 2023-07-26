using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    private readonly Vector3 _leftScale = new Vector3(-1, 1, 1);
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
        _animator.SetFloat(Speed, _movement.sqrMagnitude);
        
        if (_movement.x > 0)
        {
            transform.localScale = _leftScale;
        }
        else if (_movement.x < 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
