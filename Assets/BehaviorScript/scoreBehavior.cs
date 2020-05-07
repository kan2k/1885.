using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreBehavior : MonoBehaviour
{
    public TextMeshProUGUI scoreTextWhite;
    public TextMeshProUGUI scoreTextBlack;
    public static float score;
    void Update()
    {
        if (!pauseBehavior.isPaused && !characterBehavior.isDead)
        {
            scoreTextWhite.text = "Score: " + Mathf.Round(score).ToString();
            scoreTextBlack.text = scoreTextWhite.text;
            score += Time.deltaTime * 10;
        }
    }
    void Start()
    {
        score = 0f;
    }
}
