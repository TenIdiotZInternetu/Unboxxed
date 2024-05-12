using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1.0f;
    [SerializeField] private float jumpForce = 1.0f;
    [SerializeField] private float maxJumpHeight = 3.0f;
    
    [SerializeField] private float fallingGravityScale = 5.0f;
    [SerializeField] private float jumpCancelForce = 3.0f;

    private Transform _transform;
    private Rigidbody2D _rigidBody;
    private float _gravityScale;
    
    private Vector2 _up = Vector2.up;

    private bool _isJumping;
    private bool _isGrounded;
    
    private float _jumpHeight;
    private float _prevYPos;
    private float _airTime;
    
    private bool IsFalling => _rigidBody.velocity.y < 0;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
        _gravityScale = _rigidBody.gravityScale;
        
        Controls.Jumps += Jump;
        Controls.JumpsRelease += CancelJump;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal(Controls.MoveHorizontal);
        ApplyGravity();
        CheckJumpHeight();
    }

    private void CheckJumpHeight()
    {
        if (_isJumping)
        {
            float deltaHeight = _transform.position.y - _prevYPos;
            _jumpHeight += deltaHeight;
            
            if (_jumpHeight >= maxJumpHeight)
            {
                CancelJump();
            }
        }
        
        _prevYPos = _transform.position.y;
    }

    private void ApplyGravity()
    {
        if (IsFalling)
        {
            _rigidBody.gravityScale = fallingGravityScale;
        }
        else
        {
            _rigidBody.gravityScale = _gravityScale;
        }
    }

    private void MoveHorizontal(float value)
    {
        _rigidBody.velocity = new Vector2(value * horizontalSpeed, _rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (!_isGrounded || _isJumping) return;

        _isJumping = true;
        _rigidBody.AddForce(jumpForce * _up, ForceMode2D.Impulse);
    }

    public void CancelJump()
    {
        if (!_isJumping) return;
        
        _rigidBody.AddForce(MathF.Pow(_rigidBody.velocity.y, jumpCancelForce) * Vector2.down, ForceMode2D.Impulse);
        
        _isJumping = false;
        _jumpHeight = 0;
    }

    public void Ground(bool isGrounded)
    {
        _isGrounded = isGrounded;
    }
}
