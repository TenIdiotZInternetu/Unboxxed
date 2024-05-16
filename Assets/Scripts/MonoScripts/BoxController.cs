using System;
using System.Collections.Generic;
using Cinemachine;
using Packages.SerializableDictionary;
using SOs;
using UnityEditor;
using UnityEngine;

namespace MonoScripts
{
    [Serializable]
    public class DirectionCameraDictionary : SerializableDictionary<GravityDirectionSo, CinemachineVirtualCamera> {}
    
    public class BoxController : MonoBehaviour
    {
        [SerializeField] private DirectionCameraDictionary cameras;

        private GravityController _gravity;
        private CameraController _cameraController;

        private void Start()
        {
            _gravity = GravityController.FindInScene();
            _cameraController = CameraController.FindInScene();
        }

        public void Enter(GravityDirectionSo gravityDirection)
        {
            _gravity.RotateTo(gravityDirection);
            _cameraController.ChangeCamera(cameras[gravityDirection]);
        }
    }
}
