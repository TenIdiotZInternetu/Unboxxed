using Cinemachine;
using Packages.Tarodev_2D_Controller._Scripts;
using UnityEngine;

namespace MonoScripts
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(CinemachineBrain))]
    [RequireComponent(typeof(CameraController))]
    public class PlayerAwareCamera : MonoBehaviour
    {
        [SerializeField] private AnimationCurve velocityToBlendTime;
        [SerializeField] private AnimationCurve lookDownSpeed;

        private CinemachineBrain _brain;
        private CameraController _controller;
        private PlayerController _player;
        
        private void Awake()
        {
            _brain = GetComponent<CinemachineBrain>();
            _controller = GetComponent<CameraController>();
            _player = PlayerController.FindInScene();
            
            _controller.OnCameraChanged += AdjustBlendtime;
        }
        
        private void Update()
        {
            LookDown();
        }

        private void AdjustBlendtime(CinemachineVirtualCamera _)
        {
            var newBlendTime = velocityToBlendTime.Evaluate(_player.MovementSpeed);
            _brain.m_DefaultBlend.m_Time = newBlendTime;
        }
        
        private void LookDown()
        {
            var lookDown = lookDownSpeed.Evaluate(_player.MovementSpeed);
            _controller.transform.rotation = Quaternion.Euler(0, 0, lookDown);
        }
    }
}