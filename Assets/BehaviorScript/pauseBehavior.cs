using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseBehavior : MonoBehaviour
{
    public static bool isPaused= false;
    public static bool isPauseMenuOpened = false;
    public GameObject pauseMenu;
    public GameObject shopMenu;

    private int indexPause = 99;
    private int totalLevels = 3;

    public GameObject index0object;
    public GameObject index1object;
    public GameObject index2object;

    //FadeInFadeOut
    public Image Fade;
    public Animator anim;

    //Audio
    public AudioSource BackgroundAudio;
    public AudioSource PressAudio;
    public AudioSource SelectAudio;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !shopBehavior.isShopOpened && !characterBehavior.isDead)
        {
            if (isPaused)
            {
                BackgroundAudio.Play();
                Resume();
            }else
            {
                BackgroundAudio.Pause();
                indexPause = 0;
                Pause();
            }
        }

        if (isPaused)
        {
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) )&& !shopBehavior.isShopOpened)
            {
                if (indexPause < totalLevels - 1)
                {
                    indexPause++;
                    SelectAudio.Play();
                }
            }
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) )&& !shopBehavior.isShopOpened)
            {
                if (indexPause > 0)
                {
                    indexPause--;
                    SelectAudio.Play();
                }
            }
            if (Input.GetKeyDown(KeyCode.Return) && !shopBehavior.isShopOpened)
            {
                if (indexPause == 0)
                {
                    BackgroundAudio.Play();
                    Resume();
                    PressAudio.Play();
                }
                if (indexPause == 1)
                {
                    BackgroundAudio.Play();
                    isPauseMenuOpened = false;
                    pauseMenu.SetActive(false);
                    shopBehavior.isShopOpened = true;
                    shopMenu.SetActive(true);
                    PressAudio.Play();
                }
                if (indexPause == 2  && !shopBehavior.isShopOpened)
                {
                    isPauseMenuOpened = false;
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1f;
                    isPaused = false;
                    PressAudio.Play();
                    StartCoroutine(Fading());
                }

            }

            if (indexPause == 0)
            {
                index0object.GetComponent<Animator>().Play("menuTextAnimation");
            }
            else index0object.GetComponent<Animator>().Play("NoAnimation");

            if (indexPause == 1)
            {
                index1object.GetComponent<Animator>().Play("menuTextAnimation");
            }
            else index1object.GetComponent<Animator>().Play("NoAnimation");

            if (indexPause == 2)
            {
                index2object.GetComponent<Animator>().Play("menuTextAnimation");
            }
            else index2object.GetComponent<Animator>().Play("NoAnimation");
        }
    }

    void Resume()
    {
        index0object.GetComponent<Animator>().Rebind();
        index1object.GetComponent<Animator>().Rebind();
        index2object.GetComponent<Animator>().Rebind();
        isPauseMenuOpened = false;
        shopMenu.SetActive(false);
        pauseMenu.SetActive(false);
        shopBehavior.isShopOpened = false;
        Time.timeScale = 1f;
        isPaused = false;
        indexPause = 99;
    }

    void Pause()
    {
        isPauseMenuOpened = true;
        shopMenu.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        Debug.Log(Fade.color.a);
        yield return new WaitUntil(() => Fade.color.a == 1);
        SceneManager.LoadScene("MainMenu");
    }
}