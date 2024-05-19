using UnityEngine;
using UnityEngine.SceneManagement;

namespace SOs
{
    [CreateAssetMenu(fileName = "LevelSelect", menuName = "SOs/LevelSelect")]
    public class LevelSelectSo : ScriptableObject
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
