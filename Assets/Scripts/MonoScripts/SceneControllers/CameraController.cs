using System;
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
    
        private CinemachineBrain _brain;
        public CinemachineVirtualCamera CurrentCamera { get; private set; }

        public Action<CinemachineVirtualCamera> OnCameraChanged;
        
        private void Awake()
        {
            gameObject.tag = TAG;
            _brain = GetComponent<CinemachineBrain>();
            
            CurrentCamera = initialCamera;
            CurrentCamera.Priority = INITIAL_PRIORITY;
            OnCameraChanged?.Invoke(CurrentCamera);
        }
        
        public static CameraController FindInScene()
        {
            return GameObject.FindWithTag(TAG).GetComponent<CameraController>();
        }

        public void ChangeCamera(CinemachineVirtualCamera newCamera)
        {
            CurrentCamera.Priority = NO_PRIORITY;
            CurrentCamera = newCamera;
            CurrentCamera.Priority = ACTIVE_PRIORITY;
            
            OnCameraChanged?.Invoke(CurrentCamera);
        }
    }
}
