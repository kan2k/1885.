using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuBehavior : MonoBehaviour
{

    private int index = 0;
    private int totalLevels = 4;
    public float yOffset = 1f;

    public GameObject index0object;
    public GameObject index1object;
    public GameObject index2object;
    public GameObject index3object;

    public AudioSource menuSelectAudio;
    public AudioSource PressAudio;

    void Start()
    {
        Time.timeScale = 1f;
        Screen.SetResolution(500, 1000, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (index < totalLevels - 1)
            {
                index++;
                menuSelectAudio.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W))
        {
            if (index > 0)
            {
                index--;
                menuSelectAudio.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (index == 0)
            {
                PressAudio.Play();
                SceneManager.LoadScene("Game1");
            }
            if (index == 1)
            {
                PressAudio.Play();
                SceneManager.LoadScene("SettingsMenuu");
            }
            if (index == 2)
            {
                PressAudio.Play();
                SceneManager.LoadScene("LeaderboardMenu");
            }
            if (index == 3)
            {
                PressAudio.Play();
                Application.Quit();
            }

        }

        if (index == 0)
        {
            index0object.GetComponent<Animator>().Play("menuTextAnimation");
        } else index0object.GetComponent<Animator>().Play("NoAnimation");

        if (index == 1)
        {
            index1object.GetComponent<Animator>().Play("menuTextAnimation");
        }
        else index1object.GetComponent<Animator>().Play("NoAnimation");

        if (index == 2)
        {
            index2object.GetComponent<Animator>().Play("menuTextAnimation");
        }
        else index2object.GetComponent<Animator>().Play("NoAnimation");

        if (index == 3)
        {
            index3object.GetComponent<Animator>().Play("menuTextAnimation");
        }
        else index3object.GetComponent<Animator>().Play("NoAnimation");
    }
}
