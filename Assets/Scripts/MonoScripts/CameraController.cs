using Cinemachine;
using UnityEngine;

namespace MonoScripts
{
    public class CameraController : MonoBehaviour
    {
        private const int INITIAL_PRIORITY = 100;
        private const int ACTIVE_PRIORITY = 20;
        private const int NO_PRIORITY = 0;
    
        [SerializeField] private CinemachineVirtualCamera initialCamera;
    
        private CinemachineVirtualCamera _currentCamera;
        
        private void Awake()
        {
            _currentCamera = initialCamera;
            _currentCamera.Priority = INITIAL_PRIORITY;
        }

        public void ChangeCamera(CinemachineVirtualCamera newCamera)
        {
            _currentCamera.Priority = NO_PRIORITY;
            _currentCamera = newCamera;
            _currentCamera.Priority = ACTIVE_PRIORITY;
            
            Debug.Log(_currentCamera.Priority);
        }
    }
}
