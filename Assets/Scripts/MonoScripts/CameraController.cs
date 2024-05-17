using Cinemachine;
using UnityEngine;

namespace MonoScripts
{
    public class CameraController : MonoBehaviour
    {
        public const string TAG = "MainCamera";
        
        private const int INITIAL_PRIORITY = 100;
        private const int ACTIVE_PRIORITY = 20;
        private const int NO_PRIORITY = 0;
    
        [SerializeField] private CinemachineVirtualCamera initialCamera;
    
        private CinemachineVirtualCamera _currentCamera;
        
        private void Awake()
        {
            gameObject.tag = TAG;
            _currentCamera = initialCamera;
            _currentCamera.Priority = INITIAL_PRIORITY;
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
        }
    }
}
