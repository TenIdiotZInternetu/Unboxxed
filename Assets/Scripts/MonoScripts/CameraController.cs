using Cinemachine;
using MyBox;
using Packages.Tarodev_2D_Controller._Scripts;
using UnityEngine;

namespace MonoScripts
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(CinemachineBrain))]
    public class CameraController : MonoBehaviour
    {
        public const string TAG = "MainCamera";
        
        private const int INITIAL_PRIORITY = 100;
        private const int ACTIVE_PRIORITY = 20;
        private const int NO_PRIORITY = 0;
    
        [SerializeField] private CinemachineVirtualCamera initialCamera;
        [SerializeField] private bool adjustTransitionsToPlayer;
    
        private CinemachineBrain _brain;
        private CinemachineVirtualCamera _currentCamera;
        
        private PlayerController _player;
        
        [ConditionalField(nameof(adjustTransitionsToPlayer))]
        [SerializeField] private AnimationCurve velocityToBlendTime;
        
        private void Awake()
        {
            gameObject.tag = TAG;
            _brain = GetComponent<CinemachineBrain>();
            
            _currentCamera = initialCamera;
            _currentCamera.Priority = INITIAL_PRIORITY;

            if (adjustTransitionsToPlayer)
            {
                _player = PlayerController.FindInScene();
            }
        }
        
        public static CameraController FindInScene()
        {
            return GameObject.FindWithTag(TAG).GetComponent<CameraController>();
        }

        public void ChangeCamera(CinemachineVirtualCamera newCamera)
        {
            _currentCamera.Priority = NO_PRIORITY;
            _currentCamera = newCamera;
            _currentCamera.Priority = ACTIVE_PRIORITY;
            
            if (adjustTransitionsToPlayer)
            {
                var newBlendTime = velocityToBlendTime.Evaluate(_player.MovementSpeed);
                _brain.m_DefaultBlend.m_Time = newBlendTime;
            }
        }
    }
}
