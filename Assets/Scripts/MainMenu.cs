using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DK.UI
{
    public class MainMenu : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private GameObject menuCanvas;
        [SerializeField] private GameObject infoPanel;

        [Header("Title Animation")]
        [SerializeField] private Animator titleAnimator;
        [SerializeField] private string introAnimationName = "TitleIntro";

        [Header("Fade Transition")]
        [SerializeField] private Image fadeOverlay;
        [SerializeField] private float fadeDuration = 2f;

        [Header("Timing")]
        [SerializeField] private float delayInSeconds = 2.5f;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip playButtonSFX;
        public AudioClip infoButtonSFX;
        public AudioClip genericButtonSFX;

        private static bool hasSeenIntro = false;

        private void Start()
        {
            if (infoPanel != null) infoPanel.SetActive(false);

            if (fadeOverlay != null)
            {
                Color c = fadeOverlay.color;
                c.a = 0f;
                fadeOverlay.color = c;
                fadeOverlay.gameObject.SetActive(false);
            }

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
                if (titleAnimator != null)
                {
                    titleAnimator.Play(introAnimationName, 0, 1f);
                }

                if (menuCanvas != null)
                {
                    menuCanvas.SetActive(true);
                }
            }
        }

        private IEnumerator ShowMenuAfterDelay()
        {
            yield return new WaitForSeconds(delayInSeconds);
            if (menuCanvas != null) menuCanvas.SetActive(true);
        }

        public void ToggleInfoPanel()
        {
            if (infoPanel != null)
            {
                infoPanel.SetActive(!infoPanel.activeSelf);
            }

            if (audioSource != null && infoButtonSFX != null)
            {
                audioSource.PlayOneShot(infoButtonSFX);
            }
        }

        public void PlayGenericSound()
        {
            if (audioSource != null && genericButtonSFX != null)
            {
                audioSource.PlayOneShot(genericButtonSFX);
            }
        }

        public void PlayGame()
        {
            if (audioSource != null && playButtonSFX != null)
            {
                audioSource.PlayOneShot(playButtonSFX);
            }

            StartCoroutine(FadeAndLoadScene(1));
        }

        private IEnumerator FadeAndLoadScene(int sceneIndex)
        {
            if (fadeOverlay != null)
            {
                fadeOverlay.gameObject.SetActive(true);
                Color fadeColor = fadeOverlay.color;
                float elapsedTime = 0f;

                while (elapsedTime < fadeDuration)
                {
                    elapsedTime += Time.deltaTime;
                    fadeColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                    fadeOverlay.color = fadeColor;

                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(fadeDuration);
            }

            SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
}