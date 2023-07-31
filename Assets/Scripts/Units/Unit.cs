using UnityEngine;
using UnityEngine.Assertions;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] protected float _moveSpeed = 5f;
        [SerializeField] protected BoxCollider2D _hitbox;
        [SerializeField] protected float _maxHp;
        
        protected Rigidbody2D _rb;
        protected SpriteRenderer _spriteRenderer;
        protected Animator _animator;
        protected Vector2 _movement;
        protected float _currHp;
        protected static readonly int Speed = Animator.StringToHash("speed");
        protected static readonly int Horizontal = Animator.StringToHash("horizontal");

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currHp = _maxHp;
            
            Assert.IsNotNull(_hitbox);
            Assert.IsNotNull(_rb);
            Assert.IsNotNull(_animator);
            Assert.IsNotNull(_spriteRenderer);

            StartImpl();
        }
        
        protected virtual void StartImpl() {}

        public Vector3 GetCenter()
        {
            return _hitbox.bounds.center;
        }

        public virtual void TakeDamage(float dmg)
        {
            _currHp -= dmg;
            if (_currHp <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
