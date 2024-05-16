using System;
using System.Collections.Generic;
using Cinemachine;
using SOs;
using UnityEngine;

namespace MonoScripts
{
    public class BoxController : MonoBehaviour
    {
        [Serializable]
        private class DirectionCameraDictionary : SerializableDictionary<GravityDirectionSo, CinemachineVirtualCamera> {}
        
        [SerializeField] private GravityController gravity;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private DirectionCameraDictionary cameras;
        
        public void Enter(GravityDirectionSo gravityDirection)
        {
            gravity.RotateTo(gravityDirection);
            cameraController.ChangeCamera(cameras[gravityDirection]);
        }
    }
}
