using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip returnButtonSFX;
    public AudioClip leaderboardButtonSFX;

    public void ReturnToMainMenu()
    {
        if (audioSource != null && returnButtonSFX != null)
        {
            audioSource.PlayOneShot(returnButtonSFX);
        }
        StartCoroutine(LoadSceneWithDelay(0, returnButtonSFX));
    }

    public void ToLeaderboard()
    {
        if (audioSource != null && leaderboardButtonSFX != null)
        {
            audioSource.PlayOneShot(leaderboardButtonSFX);
        }
        StartCoroutine(LoadSceneWithDelay(2, leaderboardButtonSFX));
    }

    private IEnumerator LoadSceneWithDelay(int sceneIndex, AudioClip sfx)
    {
        float waitTime = sfx != null ? sfx.length : 0.2f;
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(sceneIndex);
    }
}