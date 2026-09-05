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

        private void Start()
        {
            // Hide the menu immediately when the scene loads
            if (menuCanvas != null)
            {
                menuCanvas.SetActive(false);
                StartCoroutine(ShowMenuAfterDelay());
            }
        }

        private IEnumerator ShowMenuAfterDelay()
        {
            // Pause this script for the specified amount of time
            yield return new WaitForSeconds(delayInSeconds);

            // Turns the menu back on
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