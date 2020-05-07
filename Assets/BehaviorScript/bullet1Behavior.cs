using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1Behavior : MonoBehaviour
{

    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;
    public GameObject Impact;

    void Update()
    {
        transform.Translate(Vector2.up * 5f * Time.deltaTime);

        if (transform.position.y >= borderTOPRIGHT.position.y + 2 || transform.position.y <= borderBOTTOMLEFT.position.y + 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally")
        {
            Destroy(gameObject);
            GameObject effectIns = (GameObject)Instantiate(Impact, transform.position, transform.rotation);
            Destroy(effectIns, 0.2f);
        }


    }
}
