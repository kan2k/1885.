using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healdroneBehavior : MonoBehaviour
{
    public LineRenderer healDrone;

    void Start()
    {
        // Set Color base on Settings
        Color32 playcolor1 = settingsBehavior.playcolor1;
        Color32 playcolor2 = settingsBehavior.playcolor2;
        healDrone.startColor = playcolor1;
        healDrone.endColor = playcolor2;
    }
    void Update()
    {
        if (!pauseBehavior.isPaused && !characterBehavior.isDead && characterBehavior.currenthealthPoints <= characterBehavior.maxhealthPoints)
        {
            characterBehavior.currenthealthPoints += Time.deltaTime * 5;
        }
    }
}
