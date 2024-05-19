using System;
using System.Collections;
using UnityEngine;

namespace MonoScripts.Animations
{
    public class Transposition : MonoBehaviour
    {
        [SerializeField] private AnimationCurve transpositionCurve;
        [SerializeField] private float transpositionDuration = 1f;
        [SerializeField] private Vector3 targetPosition;

        private Transform _transform;
        
        private Vector3 _initialPosition;
        private float _elapsedTime;

        private void Awake()
        {
            _transform = transform;
            _initialPosition = _transform.position;
        }

        public void Move()
        {
            StartCoroutine(MoveCoroutine(_initialPosition, targetPosition));
        }
        
        public void MoveBack()
        {
            StartCoroutine(MoveCoroutine(targetPosition, _initialPosition));
        }

        private IEnumerator MoveCoroutine(Vector3 from, Vector3 to)
        {
            _elapsedTime = 0;
            while (_transform.position != to)
            {
                _elapsedTime += Time.deltaTime;
                float timeProgress = _elapsedTime / transpositionDuration;
                float transitionProgress = transpositionCurve.Evaluate(timeProgress);
                
                _transform.position = Vector3.Lerp(from, to, transitionProgress);
                yield return null;
            }
        }
    }
}