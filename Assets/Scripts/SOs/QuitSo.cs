using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "Quit", menuName = "SOs/Quit")]
    public class QuitSo : ScriptableObject
    {
        public void Quit()
        {
            Application.Quit();
        }
    }
}