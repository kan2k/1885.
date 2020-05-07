using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBahvior : MonoBehaviour
{

    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;
    public GameObject Impact;

    void Update()
    {
        // About Fire Rate
        // I don't use float because it causes the bullet speed to be inconsis 
        //          because its getting the variable takes little bit of time.  -Jason Kwok
        transform.Translate(Vector2.up * 10f * Time.deltaTime);

        if (transform.position.y >= borderTOPRIGHT.position.y + 2 || transform.position.y <= borderBOTTOMLEFT.position.y + 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Ally" || col.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
            GameObject effectIns = (GameObject)Instantiate(Impact, transform.position, transform.rotation);
            Destroy(effectIns, 0.2f);
        }
        if (col.gameObject.tag == "Bad_Bullet_Laser")
        {
            Destroy(gameObject);
        }

    }


}
