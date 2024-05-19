using System;
using TMPro;
using UnityEngine;
using Utils;

namespace MonoScripts.UI
{
    public class FinishScreen : MenuScreen
    {
        [SerializeField] private TimerUI timer;
        [SerializeField] private TMP_Text finalTimeText;
        [SerializeField] private TMP_Text bestTimeText;
        
        [SerializeField] private TMP_Text currentLevelText;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            Time.timeScale = 0;
            
            if (currentLevelText is not null)
            {
                currentLevelText.text = "Level " + LevelSelect.CurrentLevel;
            }
            
            if (timer is not null && finalTimeText is not null)
            {
                finalTimeText.text = timer.FormattedTime;
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
