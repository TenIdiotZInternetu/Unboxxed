using UnityEngine;
using UnityEngine.Events;

namespace MonoScripts
{
    public class Trigger : MonoBehaviour
    {
        public UnityEvent<GameObject> onTriggerEnter;
        public UnityEvent<GameObject> onTriggerExit;

        [SerializeField] private LayerMask acceptedLayers;


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!IsOnLayer(col)) return;
            onTriggerEnter.Invoke(col.gameObject);
        }
    
        private void OnTriggerExit2D(Collider2D col)
        {
            if (!IsOnLayer(col)) return;
            onTriggerExit.Invoke(col.gameObject);
        }

        private bool IsOnLayer(Collider2D collider)
        {
            int layerNumber = collider.gameObject.layer;
            return (1 << layerNumber & acceptedLayers) != 0;
        }
    }
}
