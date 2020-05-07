using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterBombBulletSmall : MonoBehaviour
{

    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;
    public GameObject Impact;

    void Update()
    {
        transform.Translate(Vector2.up * 2f * Time.deltaTime);

        if (transform.position.y >= borderTOPRIGHT.position.y + 3 || transform.position.y <= borderBOTTOMLEFT.position.y + 1
            || transform.position.x >= borderTOPRIGHT.position.x || transform.position.x <= borderBOTTOMLEFT.position.x - 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")
        {
            Debug.Log("trig");
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
