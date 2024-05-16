using SOs;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonoScripts
{
    public class Warp : MonoBehaviour
    {
        [SerializeField] private BoxController targetBox;
        [SerializeField] private Transform targetPostition;
        [SerializeField] private GravityDirectionSo gravityDirection;
        
        public void WarpToBox(GameObject obj) {
            obj.transform.position = targetPostition.position;
            targetBox.Enter(gravityDirection);
        }
    }
}
