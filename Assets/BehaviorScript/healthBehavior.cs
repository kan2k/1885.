using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class healthBehavior : MonoBehaviour
{
    public Transform bar;
    public SpriteRenderer barColor;
    public TextMeshProUGUI HPTextBlack;
    public TextMeshProUGUI HPTextWhite;

    private float updatehp;
    void Update()
    {
        updatehp = (characterBehavior.currenthealthPoints / characterBehavior.maxhealthPoints);
        SetSize(updatehp);
    }

    public void SetSize(float sizeNormalized)
    {
        HPTextWhite.text = Mathf.Round(characterBehavior.currenthealthPoints) + "/" + Mathf.Round(characterBehavior.maxhealthPoints);
        HPTextBlack.text = HPTextWhite.text;
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color)
    {
        barColor.color = color;
    }
}
