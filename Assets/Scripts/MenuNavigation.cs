using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToLeaderboard()
    {
        SceneManager.LoadScene(2);
    }
}