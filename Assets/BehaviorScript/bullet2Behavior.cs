using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2Behavior : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;
    public GameObject Impact;
    private float alivetime;
    private float deadtime;

    void Start()
    {
        deadtime = Random.Range(1f, 3f);
    }

    void Update()
    {
        alivetime += Time.deltaTime;
        transform.Translate(Vector2.up * 2f * Time.deltaTime);
        if (alivetime >= deadtime)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);  
            Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 90));
            Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 180));
            Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 270));
            Destroy(gameObject);
        }

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
