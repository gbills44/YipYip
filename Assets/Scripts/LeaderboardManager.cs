using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public static bool IsNewHighscore(int currentScore)
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null || highscores.highscoreEntryList.Count < 10)
        {
            return true;
        }

        int lowestScore = int.MaxValue;
        foreach (HighscoreEntry entry in highscores.highscoreEntryList)
        {
            if (entry.score < lowestScore)
            {
                lowestScore = entry.score;
            }
        }

        return currentScore > lowestScore;
    }

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
        }

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();

        int limit = Mathf.Min(highscores.highscoreEntryList.Count, 10);
        for (int i = 0; i < limit; i++)
        {
            CreateHighScoreEntryTransform(highscores.highscoreEntryList[i], entryContainer, highscoreEntryTransformList, i);
        }
    }

    private void CreateHighScoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList, int index)
    {
        float templateHeight = 100f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * index);
        entryTransform.gameObject.SetActive(true);

        int rank = index + 1;
        string rankString;
        switch (rank)
        {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("PositionText").GetComponent<TextMeshProUGUI>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<TextMeshProUGUI>().text = highscoreEntry.score.ToString();
        entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = highscoreEntry.name;

        transformList.Add(entryTransform);
    }

    public static void SubmitNewScore(int score, string name)
    {
        HighscoreEntry newEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores { highscoreEntryList = new List<HighscoreEntry>() };
        }

        highscores.highscoreEntryList.Add(newEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}