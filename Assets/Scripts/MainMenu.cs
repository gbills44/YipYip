using UnityEngine;
using UnityEngine.SceneManagement;

namespace DK.UI
{
    public class MainMenu : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }

    }
}