using UnityEngine;

namespace MonoScripts
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
