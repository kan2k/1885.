using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameBehavior : MonoBehaviour
{

    public AudioSource BackgroundAudio;

    private void Update()
    {
        if(characterBehavior.isDead)
            BackgroundAudio.Stop();
    }
    void Start()
    {
        //Instantiate(mainCharacter, new Vector3 (0f, -3f, 0f), new Quaternion (0,0,0,0));
        // Screen.SetResolution(500, 1000, false);
    }


}
