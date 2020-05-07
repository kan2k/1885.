using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3BehaviorNegative : MonoBehaviour
{

    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;
    public GameObject Impact;


    void Update()
    {
        Transform center = GameObject.FindGameObjectWithTag("mainCharacterCenter").transform;

        Vector3 dir = center.position - transform.position;
        transform.Translate(dir.normalized * 3f * Time.deltaTime, Space.World);
        transform.RotateAround(center.transform.position, center.transform.forward, -40 * Time.deltaTime);
        if (transform.position.y >= borderTOPRIGHT.position.y + 2 || transform.position.y <= borderBOTTOMLEFT.position.y + 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally" || col.gameObject.tag == "Good_Bullet")
        {
            Destroy(gameObject);
            GameObject effectIns = (GameObject)Instantiate(Impact, transform.position, transform.rotation);
            Destroy(effectIns, 0.2f);
        }
    }
}