using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DK.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject menuCanvas;

        [Header("Timing")]
        [SerializeField] private float delayInSeconds = 2.5f;

        private static bool hasSeenIntro = false;

        private void Start()
        {
            if (!hasSeenIntro)
            {
                if (menuCanvas != null)
                {
                    menuCanvas.SetActive(false);
                    StartCoroutine(ShowMenuAfterDelay());
                }
                hasSeenIntro = true;
            }
            else
            {
                if (menuCanvas != null)
                {
                    menuCanvas.SetActive(true);
                }
            }
        }

        private IEnumerator ShowMenuAfterDelay()
        {
            yield return new WaitForSeconds(delayInSeconds);

            if (menuCanvas != null)
            {
                menuCanvas.SetActive(true);
            }
        }

        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}