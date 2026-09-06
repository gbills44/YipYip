using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Added this namespace to manage scenes

public class GameOverManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button submitButton;
    public PlayerController playerController;

    // Type the exact name of your leaderboard scene in the Inspector
    public string leaderboardSceneName = "Leaderboard";

    private void Start()
    {
        nameInputField.onValueChanged.AddListener(text => nameInputField.text = text.ToUpper());
        submitButton.onClick.AddListener(SubmitScore);
    }

    private void SubmitScore()
    {
        string playerName = nameInputField.text;

        if (string.IsNullOrEmpty(playerName))
        {
            playerName = "AAA";
        }

        int finalScore = Mathf.RoundToInt(playerController.get_PlayerScore());
        LeaderboardManager.SubmitNewScore(finalScore, playerName);

        // Load the leaderboard scene instead of turning off the panel
        SceneManager.LoadScene(leaderboardSceneName);
    }
}