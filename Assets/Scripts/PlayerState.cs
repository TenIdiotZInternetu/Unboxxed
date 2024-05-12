using PlayerScripts;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class PlayerState : PlayerController
{
    public static readonly GroundedState Grounded = new();
    public static readonly JumpingState Jumping = new();
    public static readonly InJumpPeakState InJumpPeak = new();
    public static readonly FallingState Falling = new();
    
    protected abstract void Enter();
    protected abstract void Exit();
    public abstract void UpdateState();

    public PlayerState ChangeState(PlayerState newState)
    {
        Exit();
        newState.Enter();
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
        RigidBody.AddForce(jumpForce * Up, ForceMode2D.Impulse);
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
        float deltaHeight = Transform.position.y - _prevYPos;
        _jumpHeight += deltaHeight;
            
        if (_jumpHeight >= maxJumpHeight)
        {
            CancelJump();
        }
            
        _prevYPos = Transform.position.y;
    }

    private void CancelJump()
    {
        RigidBody.AddForce(RigidBody.velocity.y * jumpCancelForce * Vector2.down, ForceMode2D.Impulse);
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
            
        if (_airTime >= gravityCancelTime)
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
        RigidBody.gravityScale = fallingSpeed;
    }

    public override void UpdateState() {}
        
    protected override void Exit()
    {
        RigidBody.gravityScale = 1;
    }
}