using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

namespace Units
{
    public class Unit : NetworkBehaviour
    {
        [SerializeField] protected float _moveSpeed = 5f;
        [SerializeField] protected BoxCollider2D _hitbox;
        [SerializeField] protected float _maxHp;
        
        protected Rigidbody2D _rb;
        protected SpriteRenderer _spriteRenderer;
        protected Animator _animator;
        protected Vector2 _movement;
        protected float _currHp;
        private bool _isKnocked;
        protected static readonly int Speed = Animator.StringToHash("speed");
        protected static readonly int Horizontal = Animator.StringToHash("horizontal");

        private bool _facingLeft;
        
        private void Start()
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

        // ensure that the hitbox's transform is at the center of the hitbox collider for this func to work
        public Vector2 GetCenter()
        {
            return _hitbox.transform.position;
        }
        
        public virtual void TakeDamage(float dmg)
        {
            _currHp -= dmg;
            if (_currHp <= 0)
            {
                Die();
            }
        }

        public virtual void TakeDamage(float dmg, Vector2 knockback)
        {
            _currHp -= dmg;
            if (_currHp <= 0)
            {
                Die();
            }
            else if (knockback.magnitude > 0)
            {
                _isKnocked = true;
                _rb.AddForce(knockback, ForceMode2D.Impulse);
                StartCoroutine(KnockbackFeedback());
            }
        }

        private IEnumerator KnockbackFeedback()
        {
            yield return new WaitForSeconds(UnitManager.Instance.knockbackDuration);
            _rb.velocity = Vector2.zero;
            _isKnocked = false;
        }

        public bool IsKnocked()
        {
            return _isKnocked;
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        public bool IsFacingLeft()
        {
            if (_movement.x == 0)
            {
                return _facingLeft;
            }
            
            _facingLeft = _movement.x < 0;
            return _facingLeft;
        }
    }
}
