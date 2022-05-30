using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    public Rigidbody2D Rb;
    [SerializeField] private BoxCollider2D _groundDetection;
    [SerializeField] private float _jumpPower = 3.5f;
    public float Speed = 3f;
    [SerializeField] private float _jumpTime = 1.4f;
    private int _jumpCount = 1;
    private int _collisionCount = 0;
    private float _x;
    private float _timeSinceJumpHeld = 0;
    private bool _canJump = false;
    private bool _isGrounded = false;
    public float OriginalSpeed = 0;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        OriginalSpeed = Speed;
    }

    private void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.W) && _jumpCount <= 0) return;
        if (Input.GetKey(KeyCode.W))
        {
            if (!_canJump && _jumpCount <= 0) return;
            _timeSinceJumpHeld += Time.deltaTime;
            _jumpCount = _jumpCount <= 0 ? 0 : _jumpCount - 1;
            _canJump = true;
            if (_timeSinceJumpHeld < _jumpTime) return;
            StopJump();
            return;
        }
        StopJump();
    }

    private void FixedUpdate()
    {
        if (_groundDetection == null) return;
        Rb.velocity = new Vector2(_x * Speed, Rb.velocity.y);
        if (!_canJump) return;
        Rb.velocity = new Vector2(Rb.velocity.x, _jumpPower);
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

    private void StopJump()
    {
        _timeSinceJumpHeld = 0;
        _canJump = false;
        if (!_isGrounded || _jumpCount == 1) return;
        _jumpCount++;
    }
}
