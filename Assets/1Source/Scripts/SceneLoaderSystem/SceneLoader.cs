using UnityEngine.SceneManagement;

namespace Scripts.SceneLoaderSystem
{
    public class SceneLoader
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}