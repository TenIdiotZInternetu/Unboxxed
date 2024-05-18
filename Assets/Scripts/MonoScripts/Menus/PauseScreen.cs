using System;
using UnityEngine;

namespace MonoScripts.Menus
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