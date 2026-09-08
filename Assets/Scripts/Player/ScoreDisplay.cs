using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] private TMP_Text scoreText;

    private float playerScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScore = player.get_PlayerScore();
    }

    // Update is called once per frame
    void Update()
    {
        playerScore = player.get_PlayerScore();
        scoreText.text = playerScore.ToString();
    }


}
