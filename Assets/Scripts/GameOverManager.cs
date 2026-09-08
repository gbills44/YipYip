using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("New Highscore UI")]
    public GameObject highscorePanel;
    public TMP_InputField nameInputField;
    public Button submitButton;

    [Header("Standard Game Over UI")]
    public GameObject standardPanel;
    public Button restartButton;

    [Header("References")]
    public PlayerController playerController;
    public string leaderboardSceneName = "Leaderboard";

    private void Start()
    {
        nameInputField.onValueChanged.AddListener(text => nameInputField.text = text.ToUpper());
        submitButton.onClick.AddListener(SubmitScore);
        restartButton.onClick.AddListener(RestartGame);

        highscorePanel.SetActive(false);
        standardPanel.SetActive(false);
    }

    public void TriggerGameOverUI()
    {
        int finalScore = Mathf.RoundToInt(playerController.get_PlayerScore());

        if (LeaderboardManager.IsNewHighscore(finalScore))
        {
            highscorePanel.SetActive(true);
        }
        else
        {
            standardPanel.SetActive(true);
        }
    }

    private void SubmitScore()
    {
        string playerName = nameInputField.text;
        if (string.IsNullOrEmpty(playerName)) playerName = "AAA";

        int finalScore = Mathf.RoundToInt(playerController.get_PlayerScore());
        LeaderboardManager.SubmitNewScore(finalScore, playerName);
        SceneManager.LoadScene(leaderboardSceneName);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}