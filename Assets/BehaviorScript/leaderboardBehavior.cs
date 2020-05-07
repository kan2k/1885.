using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class leaderboardBehavior : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public TextMeshProUGUI posText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    private List<Transform> highscoreEntryTransformList;

    void Start()
    {
        entryTemplate.gameObject.SetActive(false);
        /*highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry { name = "xxx", score = 2},
            new HighscoreEntry { name = "xxx", score = 3},
            new HighscoreEntry { name = "xxx", score = 1},
            new HighscoreEntry { name = "xxx", score = 4},
            new HighscoreEntry { name = "xxx", score = 8},
            new HighscoreEntry { name = "xxx", score = 6},
            new HighscoreEntry { name = "xxx", score = 7},
            new HighscoreEntry { name = "xxx", score = 5},
            new HighscoreEntry { name = "xxx", score = 9888888},
        }; */

        string jsonString = PlayerPrefs.GetString("public");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            jsonString = PlayerPrefs.GetString("public");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = 0; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        /*
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            {
                Debug.Log(PlayerPrefs.GetString("leaderboard2"));
                highscores.highscoreEntryList.RemoveAt((highscores.highscoreEntryList.Count -1));
            }
        }
        */

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

    } 
    void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -50f * (transformList.Count - 1));
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "th"; break;
            case 1:
                rankString = "1st"; break;
            case 2:
                rankString = "2nd"; break;
            case 3:
                rankString = "3rd"; break;
        }
        posText.text = rankString;
        string name = highscoreEntry.name;
        nameText.text = highscoreEntry.name;
        float score = highscoreEntry.score;
        scoreText.text = score.ToString();

        transformList.Add(entryTransform);

    }

    public static void AddHighscoreEntry(string name, float score)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { name = name, score = score };

        string jsonString = PlayerPrefs.GetString("public");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        highscores.highscoreEntryList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("public", json);
        PlayerPrefs.Save();
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
    [System.Serializable]
    private class HighscoreEntry
    {
        public string name;
        public float score;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        if ((Input.GetKey("escape")))
        {
            BackToMainMenu();
        }
    }
}



