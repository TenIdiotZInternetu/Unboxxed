using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private float maxJumpHeight = 3.0f;

    private Transform _transform;
    private Rigidbody2D _rigidBody;
    private LayerMask _groundLayer;
    
    private Vector2 _up = Vector2.up;

    private bool _isJumping;
    private bool _isGrounded;
    private float _jumpHeight;
    
    private bool IsFalling => _rigidBody.velocity.y < 0;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
        _groundLayer = LayerMask.NameToLayer("Ground");
        
        Controls.Jumps += Jump;
        Controls.JumpsRelease += CancelJump;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal(Controls.MoveHorizontal);
    }

    private void MoveHorizontal(float value)
    {
        _rigidBody.velocity = new Vector2(value * horizontalSpeed, _rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (!_isJumping && !_isGrounded) return;

        _isJumping = true;
        
        _rigidBody.AddForce(jumpForce * _up);
        _jumpHeight += _rigidBody.velocity.y * Time.deltaTime;

        if (_jumpHeight >= maxJumpHeight)
        {
            _isJumping = false;
            _jumpHeight = 0;
        }
    }

    public void CancelJump()
    {
        if (!_isJumping) return;
        
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        
        _isJumping = false;
        _jumpHeight = 0;
    }

    public void Ground(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }
}
