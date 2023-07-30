using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Horizontal = Animator.StringToHash("horizontal");

    void Update()
    {
        // TODO: use new unity input system
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        _animator.SetFloat(Horizontal, _movement.x);
        _animator.SetFloat(Speed, _movement.sqrMagnitude);

        _spriteRenderer.flipX = _movement.x < 0;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
