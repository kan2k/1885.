using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class gameoverBehavior : MonoBehaviour
{

    private int index = 0;
    private int totalLevels = 2;

    public GameObject index0object;
    public GameObject index1object;
    public TextMeshProUGUI nameInputField;
    public TextMeshProUGUI ScoreText;
    public GameObject buttonSave;
    public GameObject savedText;
    public GameObject fade;
    private bool InGameOverScreen;

    //FadeInFadeOut
    public Image Fade;
    public Animator anim;

    //Adio
    public AudioSource SelectAudio;
    public AudioSource PressAudio;

    void Update()
    {
        if (characterBehavior.isDead)
        {
            ScoreText.text = "Your Score: " + Mathf.Round(scoreBehavior.score);

            if (!InGameOverScreen)
            {
                fade.SetActive(false);
                buttonSave.SetActive(true);
                savedText.SetActive(false);
                InGameOverScreen = true;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (index < totalLevels - 1)
                {
                    index++;
                    SelectAudio.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (index > 0)
                {
                    index--;
                    SelectAudio.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (index == 0)
                {
                    PressAudio.Play();
                    characterBehavior.currenthealthPoints = 100;
                    characterBehavior.isDead = false;
                    Retry();
                }
                if (index == 1)
                {
                    PressAudio.Play();
                    characterBehavior.currenthealthPoints = 100;
                    Time.timeScale = 1f;
                    characterBehavior.isDead = false;
                    StartCoroutine(Fading());
                }
            }

            if (index == 0)
            {
                index0object.GetComponent<Animator>().Play("menuTextAnimation");
            }
            else index0object.GetComponent<Animator>().Play("NoAnimation");

            if (index == 1)
            {
                index1object.GetComponent<Animator>().Play("menuTextAnimation");
            }
            else index1object.GetComponent<Animator>().Play("NoAnimation");
        }
    }

    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SaveScores()
    {
        savedText.SetActive(true);
        buttonSave.SetActive(false);
        leaderboardBehavior.AddHighscoreEntry(nameInputField.text, Mathf.Round(scoreBehavior.score));
    }

    IEnumerator Fading()
    {
        Time.timeScale = 1f;
        fade.SetActive(true);
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Fade.color.a == 1);
        SceneManager.LoadScene("MainMenu");
    }
}
