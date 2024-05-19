using System.Collections;
using System.Collections.Generic;
using MonoScripts.SceneControllers;
using Unity.VisualScripting;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private const float FPS = 30f;
    
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private float rotationDuration = 1f;
    
    [SerializeField] private bool rotateToGravity = true;
    
    private GravityController _gravity;
    private Transform _transform;
    
    private Quaternion _currentRotation;
    private Quaternion _targetRotation;
    private float _elapsedTime;
    
    private void Awake()
    {
        _transform = transform;
        _gravity = GravityController.FindInScene();
        
        _transform.rotation = _gravity.RotationQuaternion;
        _currentRotation = _transform.rotation;

        if (rotateToGravity)
        {
            _gravity.OnGravityChanged += RotateToGravity;
        }
    }

    public void RotateToGravity()
    {
        RotateTo(_gravity.RotationQuaternion);
    }
    
    public void RotateTo(float targetDegrees)
    {
        _targetRotation = Quaternion.Euler(0, 0, targetDegrees);
        StartCoroutine(RotateCoroutine());
    }
    
    public void RotateTo(Quaternion targetRotation)
    {
        _targetRotation = targetRotation;
        StartCoroutine(RotateCoroutine());
    }
    
    public void RotateBy(float degrees)
    {
        _targetRotation = _currentRotation * Quaternion.Euler(0, 0, degrees);
        StartCoroutine(RotateCoroutine());
    }
    
    public void RotateBy(Quaternion rotation)
    {
        _targetRotation = _currentRotation * rotation;
        StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        while (_elapsedTime < rotationDuration)
        {
            _elapsedTime += Time.deltaTime;
            float timeProgress = _elapsedTime / rotationDuration;
            float rotationProgress = rotationCurve.Evaluate(timeProgress);
            
            _transform.rotation = Quaternion.Slerp(_currentRotation, _targetRotation, rotationProgress);
            yield return null;
        }
        
        _currentRotation = _transform.rotation;
        _elapsedTime = 0;
    }
}
