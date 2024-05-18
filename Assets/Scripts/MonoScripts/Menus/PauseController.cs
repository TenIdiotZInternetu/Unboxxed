using UnityEngine;

namespace MonoScripts.Menus
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] PauseScreen pauseScreen;
        
        private void Awake()
        {
            pauseScreen.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            
        }
    }
}