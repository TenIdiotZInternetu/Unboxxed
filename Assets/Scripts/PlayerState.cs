using System;
using PlayerScripts;
using UnityEngine;

public abstract class PlayerState
{
    public static readonly GroundedState Grounded = new();
    public static readonly JumpingState Jumping = new();
    public static readonly InJumpPeakState InJumpPeak = new();
    public static readonly FallingState Falling = new();
    
    protected static PlayerController Player;
    protected static Rigidbody2D RigidBody;
    
    public event Action<PlayerState> StateChanged;
    
    public static PlayerState Initialize(PlayerController player)
    {
        Player = player;
        RigidBody = player.GetComponent<Rigidbody2D>();
        return Falling;
    }
    
    protected abstract void Enter();
    protected abstract void Exit();
    public abstract void UpdateState();

    public PlayerState ChangeState(PlayerState newState)
    {
        Exit();
        newState.Enter();
        
        StateChanged?.Invoke(newState);
        return newState;
    }
}
    
public class GroundedState : PlayerState
{
    protected override void Enter()
    {
        Controls.Jumps += Jump;
    }

    public override void UpdateState()
    {
        if (RigidBody.velocity.y < 0)
        {
            ChangeState(Falling);
        }
    }
        
    protected override void Exit()
    {
        Controls.Jumps -= Jump;
    }

    private void Jump()
    {
        RigidBody.AddForce(Player.jumpForce * Vector2.up, ForceMode2D.Impulse);
        ChangeState(Jumping);
    }
}
    
public class JumpingState : PlayerState
{
    private float _prevYPos;
    private float _jumpHeight;
        
    protected override void Enter()
    {
        Controls.JumpsRelease += CancelJump;
    }

    public override void UpdateState()
    {
        CheckJumpHeight();
    }
        
    protected override void Exit()
    {
        Controls.JumpsRelease -= CancelJump;
    }

    private void CheckJumpHeight()
    {
        float deltaHeight = Player.transform.position.y - _prevYPos;
        _jumpHeight += deltaHeight;
            
        if (_jumpHeight >= Player.maxJumpHeight)
        {
            CancelJump();
        }
            
        _prevYPos = Player.transform.position.y;
    }

    private void CancelJump()
    {
        RigidBody.AddForce(RigidBody.velocity.y * Player.jumpCancelForce * Vector2.down, ForceMode2D.Impulse);
        ChangeState(InJumpPeak);
    }
}
    
public class InJumpPeakState : PlayerState
{
    private float _airTime = 0;
        
    protected override void Enter()
    {
        RigidBody.gravityScale = 0.1f;
    }

    public override void UpdateState()
    {
        _airTime += Time.deltaTime;
            
        if (_airTime >= Player.gravityCancelTime)
        {
            ChangeState(Falling);
        }
    }
        
    protected override void Exit()
    {
        RigidBody.gravityScale = 1;
    }
}
    
public class FallingState : PlayerState
{
    protected override void Enter()
    {
        RigidBody.gravityScale = Player.fallingSpeed;
    }

    public override void UpdateState() {}
        
    protected override void Exit()
    {
        RigidBody.gravityScale = 1;
    }
}