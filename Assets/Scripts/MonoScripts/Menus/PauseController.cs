using UnityEngine;

namespace MonoScripts.Menus
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] PauseScreen pauseScreen;
        
        private void Awake()
        {
            pauseScreen.gameObject.SetActive(false);
            Controls.Cancel += TogglePause;
        }
        
        private void TogglePause()
        {
            bool isActive = pauseScreen.gameObject.activeSelf;
            Debug.Log(isActive);
            
            if (isActive)
            {
                pauseScreen.CloseMenu();
            }
            else
            {
                pauseScreen.OpenMenu();
            }
        }
    }
}