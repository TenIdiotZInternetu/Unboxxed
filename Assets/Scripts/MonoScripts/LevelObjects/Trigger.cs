using UnityEngine;
using UnityEngine.Events;

namespace MonoScripts.LevelObjects
{
    public class Trigger : MonoBehaviour
    {
        public UnityEvent<GameObject> onTriggerEnter;
        public UnityEvent<GameObject> onTriggerExit;

        [SerializeField] private LayerMask acceptedLayers;
        [SerializeField] private bool singleUse;

        private bool _used;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_used && singleUse) return;
            if (!IsOnLayer(col)) return;
            onTriggerEnter.Invoke(col.gameObject);
        }
    
        private void OnTriggerExit2D(Collider2D col)
        {
            if (_used && singleUse) return;
            if (!IsOnLayer(col)) return;
            onTriggerExit.Invoke(col.gameObject);

            _used = true;
        }

        private bool IsOnLayer(Collider2D collider)
        {
            int layerNumber = collider.gameObject.layer;
            return (1 << layerNumber & acceptedLayers) != 0;
        }
    }
}
