using MonoScripts.SceneControllers;
using UnityEngine;
using Utils;

namespace MonoScripts.Animations
{
    public class Floating : MonoBehaviour
    {
        [SerializeField] private AnimationCurve amplitudeCurve;
        [SerializeField] private float speed = 1;
        [SerializeField] private float multiplier = 1;
        [SerializeField] private bool followGravity = true;

        private Transform _transform;
        private GravityController _gravity;
    
        private Vector3 _origin;
    
        void Start()
        {
            _transform = transform;
            _origin = _transform.position;
        }
    
        void Update()
        {
            float yOffset = amplitudeCurve.Evaluate(Time.time * speed) * multiplier;
            Vector2 upVector = followGravity ? _gravity.Up : Vector2.up;
            _transform.position = _origin + (yOffset * upVector).To3d();
        }
    }
}
