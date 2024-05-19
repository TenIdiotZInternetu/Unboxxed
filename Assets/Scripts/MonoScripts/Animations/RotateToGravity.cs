using System.Collections;
using System.Collections.Generic;
using MonoScripts.SceneControllers;
using Unity.VisualScripting;
using UnityEngine;

public class RotateToGravity : MonoBehaviour
{
    private const float FPS = 30f;
    
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private float rotationDuration = 1f;
    
    private GravityController _gravity;
    private Transform _transform;
    
    private Quaternion _currentRotation;
    private float _elapsedTime;
    
    private void Awake()
    {
        _transform = transform;
        _gravity = GravityController.FindInScene();
        
        _transform.rotation = _gravity.RotationQuaternion;
        _currentRotation = _transform.rotation;
        _gravity.OnGravityChanged += Rotate;
    }
    
    private void Rotate()
    {
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        while (_elapsedTime < rotationDuration)
        {
            _elapsedTime += Time.deltaTime;
            float timeProgress = _elapsedTime / rotationDuration;
            float rotationProgress = rotationCurve.Evaluate(timeProgress);
            
            _transform.rotation = Quaternion.Slerp(_currentRotation, _gravity.RotationQuaternion, rotationProgress);
            yield return null;
        }
        
        _currentRotation = _transform.rotation;
        _elapsedTime = 0;
    }
}
