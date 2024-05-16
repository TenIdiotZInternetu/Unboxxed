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
