using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _groundDetection;
    [SerializeField] private float _jumpPower = 3.5f;
    [SerializeField] private float _speed = 3f;
    private int _collisionCount = 0;
    private float _x, _y;
    private bool _canJump = false;
    private bool _isGrounded = false;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            _canJump = true;
            return;
        }
        _canJump = false;
    }

    private void FixedUpdate()
    {
        if (_groundDetection == null) return;
        _rb.velocity = new Vector2(_x * _speed, _rb.velocity.y);
        if (!_canJump || !_isGrounded) return;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Wall")) return;
        _collisionCount++;
        _isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Wall")) return;
        _collisionCount--;
        if (_collisionCount >= 1) return;
        _isGrounded = false;
    }
}
