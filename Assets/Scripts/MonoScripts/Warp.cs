using UnityEngine;

namespace MonoScripts
{
    public class Warp : MonoBehaviour
    {
        public void WarpHere(GameObject obj) {
            obj.transform.position = transform.position;
        }
    }
}
