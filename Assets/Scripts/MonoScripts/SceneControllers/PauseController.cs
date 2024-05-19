using System;
using MonoScripts.UI;
using UnityEngine;

namespace MonoScripts.SceneControllers
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] PauseScreen pauseScreen;
        
        private void OnEnable()
        {
            pauseScreen.gameObject.SetActive(false);
            Controls.Cancel += TogglePause;
        }
        
        private void TogglePause()
        {
            bool isActive = pauseScreen.gameObject.activeSelf;
            
            if (isActive)
            {
                pauseScreen.CloseMenu();
            }
            else
            {
                pauseScreen.OpenMenu();
            }
        }

        private void OnDisable()
        {
            Controls.Cancel -= TogglePause;
        }
    }
}