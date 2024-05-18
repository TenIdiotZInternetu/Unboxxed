using UnityEngine.SceneManagement;

namespace Utils
{
    public static class LevelSelect
    {
        public static int CurrentLevel { get; private set; } = 1;
        
        public static void ChangeLevel(int level)
        {
            SceneManager.LoadScene("Level" + level);
            CurrentLevel = level;
        }
        
        public static void LoadNextLevel()
        {
            ChangeLevel(CurrentLevel + 1);
        }
    }
}
