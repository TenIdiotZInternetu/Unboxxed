using PlayerScripts;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody2D))]
public partial class PlayerController : MonoBehaviour
{
    [SerializeField] public float horizontalSpeed = 1.0f;
    [SerializeField] public float jumpForce = 1.0f;
    [SerializeField] public float maxJumpHeight = 3.0f;
    
    [SerializeField] public float gravityCancelTime = 0.3f;
    [SerializeField] public float fallingSpeed = 5.0f;
    [SerializeField] public float jumpCancelForce = 3.0f;

    protected Transform Transform;
    protected Rigidbody2D RigidBody;
    protected Vector2 Up = Vector2.up;

    private PlayerState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        Transform = transform;
        RigidBody = GetComponent<Rigidbody2D>();
        
        _currentState = PlayerState.Initialize(this);
        _currentState.StateChanged += ChangeState;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal(Controls.MoveHorizontal);
        _currentState.UpdateState();
        Debug.Log(_currentState);
    }

    private void MoveHorizontal(float value)
    {
        RigidBody.velocity = new Vector2(value * horizontalSpeed, RigidBody.velocity.y);
    }

    public void Ground(bool isGrounded)
    {
        if (_currentState is JumpingState) return;
        _currentState = _currentState.ChangeState(PlayerState.Grounded);
    }
    
    public void ChangeState(PlayerState state)
    {
        _currentState = state;
    }
}
