using System.Collections;
using System.Collections.Generic;
using MonoScripts.SceneControllers;
using UnityEngine;

public class RotateToGravity : MonoBehaviour
{
    private GravityController _gravity;
    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
        _gravity = GravityController.FindInScene();
        _gravity.OnGravityChanged += Rotate;
    }
    
    private void Rotate()
    {
        _transform.rotation = _gravity.RotationQuaternion;
    }
}
