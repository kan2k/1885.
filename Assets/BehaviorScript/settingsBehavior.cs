using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;

public class settingsBehavior : MonoBehaviour
{
    public LineRenderer character;
    public static int CharacterColorIndex = 0;
    private int totalColors = 9;
    public Slider slider;
    public Toggle toggle;
    public AudioMixer audioMixer;
    public TextMeshProUGUI indextext;
    public static Color playcolor1 = Color.white;
    public static Color playcolor2 = Color.white;
    public static bool autoFireToggle = false;


    void Start()
    {
        indextext.text = "Type " + CharacterColorIndex;
        character.startColor = playcolor1;
        character.endColor = playcolor2;
        slider.value = PlayerPrefs.GetFloat("Volume", 0.2f);
        toggle.isOn = autoFireToggle;
    }

    public void ToggleAutoFire(bool newValue)
    {
        autoFireToggle = newValue;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void NextColor()
    {
        if (CharacterColorIndex < totalColors)
        {
            CharacterColorIndex++;
            indextext.text = "Type " + CharacterColorIndex;
        }
    }

    public void PreviousColor()
    {
        if (CharacterColorIndex > 0)
        {
            CharacterColorIndex--;
            indextext.text = "Type " + CharacterColorIndex;
        }
    }
    private void Update()
    {
        if ((Input.GetKey("escape")))
        {
            SceneManager.LoadScene("MainMenu");
        }

        character.startColor = playcolor1;
        character.endColor = playcolor2;

        if (CharacterColorIndex == 0)
        {
            playcolor1 = Color.white;
            playcolor2 = Color.white;
        }
        if (CharacterColorIndex == 1)
        {
            playcolor1 = Color.red;
            playcolor2 = Color.red;
        }
        if (CharacterColorIndex == 2)
        {
            playcolor1 = new Color32(218, 104, 15, 255);
            playcolor2 = new Color32(218, 104, 15, 255);
        }
        if (CharacterColorIndex == 3)
        {
            playcolor1 = Color.yellow;
            playcolor2 = Color.yellow;
        }
        if (CharacterColorIndex == 4)
        {
            playcolor1 = Color.green;
            playcolor2 = Color.green;
        }
        if (CharacterColorIndex == 5)
        {
            playcolor1 = Color.cyan;
            playcolor2 = Color.cyan;
        }
        if (CharacterColorIndex == 6)
        {
            playcolor1 = Color.blue;
            playcolor2 = Color.blue;
        }
        if (CharacterColorIndex == 7)
        {
            playcolor1 = Color.magenta;
            playcolor2 = Color.magenta;
        }
        if (CharacterColorIndex == 8)
        {
            playcolor1 = Color.black;
            playcolor2 = Color.white;
        }
        if (CharacterColorIndex == 9)
        {
            playcolor1 = Color.red;
            playcolor2 = Color.blue;
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
