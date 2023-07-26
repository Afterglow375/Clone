using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Animator _animator;
    private readonly Vector3 _leftScale = new Vector3(-1, 1, 1); 
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int Vertical = Animator.StringToHash("vertical");
    private static readonly int Horizontal = Animator.StringToHash("horizontal");

    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        _animator.SetFloat(Horizontal, _movement.x);
        _animator.SetFloat(Vertical, _movement.y);
        _animator.SetFloat(Speed, _movement.sqrMagnitude);
        
        if (_movement.x < 0)
        {
            transform.localScale = _leftScale;
        }
        else if (_movement.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
