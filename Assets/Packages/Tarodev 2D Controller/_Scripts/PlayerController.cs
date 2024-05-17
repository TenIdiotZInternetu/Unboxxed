using System;
using MonoScripts;
using UnityEngine;

namespace Packages.Tarodev_2D_Controller._Scripts
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        public const string TAG = "Player";
        
        [SerializeField] private ScriptableStats stats;
        [SerializeField] private Collider2D groundCollider;
        [SerializeField] private Collider2D ceilingCollider;

        private GravityController _gravity;
        
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private bool _cachedQueryStartInColliders;

        #region Interface

        public float MovementSpeed => _frameVelocity.magnitude;
        public bool IsRising => _frameVelocity.y > 0;
        public bool IsFalling => _frameVelocity.y < 0;
        
        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private float _time;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();
            _gravity = GravityController.FindInScene();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
            _gravity.OnGravityChanged += ApplyGravityDirection;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            GatherInput();
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
            };

            if (stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }
        }
        
        public static PlayerController FindInScene()
        {
            return GameObject.FindWithTag(TAG).GetComponent<PlayerController>();
        }

        private void FixedUpdate()
        {
            _frameVelocity = _gravity.ApplyInverse(_rb.velocity);
            CheckCollisions();

            HandleJump();
            HandleDirection();
            HandleGravity();
            
            ApplyMovement();
        }

        #region Collisions
        
        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;
        private bool _inJump;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;
            
            // Ground and Ceiling
            bool groundHit = groundCollider.IsTouchingLayers(~stats.PlayerLayer);
            bool ceilingHit = ceilingCollider.IsTouchingLayers(~stats.PlayerLayer);
            
            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _releasedJumpEarly = false;
                _inJump = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion

        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _releasedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + stats.CoyoteTime;

        private void HandleJump()
        {
            if (IsFalling) _inJump = false;
            
            _releasedJumpEarly = _inJump && !_frameInput.JumpHeld;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _releasedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _inJump = true;
            _frameVelocity.y = stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? stats.GroundDeceleration : stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * stats.MaxSpeed, stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Gravity
        
        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = stats.GroundingForce;
            }
            else
            {
                var inAirGravity = stats.FallAcceleration;
                if (_releasedJumpEarly && _frameVelocity.y > 0) inAirGravity *= stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyGravityDirection()
        {
            transform.rotation = _gravity.RotationQuaternion;
        }

        private void ApplyMovement()
        {
            _rb.velocity = _gravity.ApplyMatrix(_frameVelocity);
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public Vector2 FrameInput { get; }
    }
}