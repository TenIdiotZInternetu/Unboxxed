using System;
using Cinemachine;
using Packages.Tarodev_2D_Controller._Scripts;
using UnityEngine;

namespace MonoScripts.SceneControllers
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
        private GravityController _gravity;
        
        private CinemachineTransposer _currentCameraTransposer;
        
        private void Awake()
        {
            _brain = GetComponent<CinemachineBrain>();
            _controller = GetComponent<CameraController>();
            
            _player = PlayerController.FindInScene();
            _gravity = GravityController.FindInScene();
            
            _controller.OnCameraChanged += AdjustBlendtime;
            _controller.OnCameraChanged += GetTransposer;
        }

        private void Start()
        {
            GetTransposer(_controller.CurrentCamera);
        }

        private void Update()
        {
            LookDown();
        }

        private void LookDown()
        {
            var lookDown = lookDownSpeed.Evaluate(Controls.MoveVertical);
            var lookDownVector = new Vector2(0, lookDown);
            float zValue = _currentCameraTransposer.m_FollowOffset.z;
            
            _currentCameraTransposer.m_FollowOffset = _gravity.ApplyMatrix(lookDownVector);
            _currentCameraTransposer.m_FollowOffset.z = zValue;
        }
        
        private void AdjustBlendtime(CinemachineVirtualCamera _)
        {
            var newBlendTime = velocityToBlendTime.Evaluate(_player.MovementSpeed);
            _brain.m_DefaultBlend.m_Time = newBlendTime;
        }
        
        private void GetTransposer(CinemachineVirtualCamera newCamera)
        {
            _currentCameraTransposer = newCamera.GetCinemachineComponent<CinemachineTransposer>();
        }
    }
}