using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class centertext : MonoBehaviour
{
    public TextMeshProUGUI CenterText;
    public Animator CenterTextAnimator;
    public AudioSource CenterTextAudio;
    public static int stage;

    void Start()
    {
        stage = 1;
    }

    void Update()
    {
        float roundedScore = Mathf.Round(scoreBehavior.score);
        if (roundedScore == 5)
        {
            CenterText.text = "Leaving Earth";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
        if (roundedScore == 250)
        {
            CenterText.text = "250";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
        if (roundedScore == 500)
        {
            CenterText.text = "500";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
        if (roundedScore == 1000)
        {
            stage = 1;
            CenterText.text = "1000";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
        if (roundedScore == 2000)
        {
            stage = 2;
            CenterText.text = "2000";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
        if (roundedScore == 5000)
        {
            stage = 3;
            CenterText.text = "5000";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
        if (roundedScore == 10000)
        {
            stage = 4;
            CenterText.text = "10000";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
        if (roundedScore == 100000)
        {
            CenterText.text = "100000";
            CenterTextAnimator.Play("FadeOutAnimation");
            CenterTextAudio.Play();
        }
    }
}
