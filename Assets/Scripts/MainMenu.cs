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

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip buttonClickSFX;

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

        public void PlayButtonSound()
        {
            if (audioSource != null && buttonClickSFX != null)
            {
                audioSource.PlayOneShot(buttonClickSFX);
            }
        }

        public void PlayGame()
        {
            PlayButtonSound();
            StartCoroutine(LoadSceneWithDelay());
        }

        private IEnumerator LoadSceneWithDelay()
        {
            float waitTime = buttonClickSFX != null ? buttonClickSFX.length : 0.2f;
            yield return new WaitForSeconds(waitTime);

            SceneManager.LoadSceneAsync(1);
        }
    }
}