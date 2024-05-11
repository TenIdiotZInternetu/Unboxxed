using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 1.0f;
    [SerializeField] private float jumpSpeed = 1.0f;

    private Rigidbody2D _rigidBody;
    
    private Vector2 _up = Vector2.up; 
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        
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
        _rigidBody.AddForce(jumpSpeed * _up);
    }

    private void CancelJump()
    {
        
    }
}
