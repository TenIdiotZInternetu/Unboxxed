using PlayerScripts;
using UnityEngine;

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
