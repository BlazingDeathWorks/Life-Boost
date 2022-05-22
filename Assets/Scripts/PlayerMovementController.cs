using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float _jumpPower = 3.5f;
    [SerializeField] private float _speed = 3f;
    private float _x, _y;
    private bool _canJump = false;
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
        _rb.velocity = new Vector2(_x * _speed, _rb.velocity.y);
        if (!_canJump) return;
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
    }
}
