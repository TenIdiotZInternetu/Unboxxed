using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class LevelSelect : MonoBehaviour
    {
        public static int CurrentLevel { get; private set; } = 1;
        
        public void ChangeLevel(int level)
        {
            SceneManager.LoadScene("Level" + level);
            CurrentLevel = level;
        }
        
        public void LoadNextLevel()
        {
            ChangeLevel(CurrentLevel + 1);
        }

        public void ResetLevel()
        {
            ChangeLevel(CurrentLevel);
        }
    }
}
