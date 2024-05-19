using System;
using Cinemachine;
using MonoScripts.SceneControllers;
using Packages.SerializableDictionary;
using SOs;
using UnityEngine;

namespace MonoScripts.LevelObjects
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
