using UnityEngine;

namespace MonoScripts.UI
{
    public class PauseScreen : MenuScreen
    {
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}