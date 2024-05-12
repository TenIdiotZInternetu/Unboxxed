using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public partial class PlayerController : MonoBehaviour
{
    [SerializeField] protected float horizontalSpeed = 1.0f;
    [SerializeField] protected float jumpForce = 1.0f;
    [SerializeField] protected float maxJumpHeight = 3.0f;
    
    [SerializeField] protected float gravityCancelTime = 0.3f;
    [SerializeField] protected float fallingSpeed = 5.0f;
    [SerializeField] protected float jumpCancelForce = 3.0f;

    protected Transform Transform;
    protected Rigidbody2D RigidBody;
    protected Vector2 Up = Vector2.up;

    private PlayerState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        Transform = transform;
        RigidBody = GetComponent<Rigidbody2D>();
        _currentState = PlayerState.Falling;
        _currentState.ChangeState(PlayerState.Falling);
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal(Controls.MoveHorizontal);
        _currentState.UpdateState();
    }

    private void MoveHorizontal(float value)
    {
        RigidBody.velocity = new Vector2(value * horizontalSpeed, RigidBody.velocity.y);
    }

    public void Ground(bool isGrounded)
    {
        _currentState = _currentState.ChangeState(PlayerState.Grounded);
    }
}
