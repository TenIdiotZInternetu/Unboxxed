using SOs;
using UnityEngine;

namespace MonoScripts
{
    public class Warp : MonoBehaviour
    {
        [SerializeField] private BoxController box;
        [SerializeField] private Transform warpTarget;
        [SerializeField] private GravityDirectionSo gravityDirection;
        
        public void WarpToBox(GameObject obj) {
            obj.transform.position = warpTarget.position;
            box.Enter(gravityDirection);
        }
    }
}
