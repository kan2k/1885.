using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterBombBullet : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;
    public GameObject Impact;
    public GameObject ImpactBomb;
    private float alivetime;
    private float deadtime;

    void Start()
    {
        deadtime = Random.Range(1f, 2f);
    }
    void Update()
    {
        alivetime += Time.deltaTime;
        transform.Translate(Vector2.up * 4f * Time.deltaTime);
        if (alivetime >= deadtime)
        {
            Shoot();
        }

        if (transform.position.y >= borderTOPRIGHT.position.y + 5 || transform.position.y <= borderBOTTOMLEFT.position.y + 1)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 45));
        Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 90));
        Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 135));
        Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 180));
        Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 225));
        Instantiate(bulletPrefab, transform.position, transform.rotation *= Quaternion.Euler(0, 0, 270));
        Destroy(gameObject);
        GameObject effectInsBomb = (GameObject)Instantiate(ImpactBomb, transform.position, transform.rotation);
        Destroy(effectInsBomb, 0.2f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss")
        {
            Shoot();
            GameObject effectIns = (GameObject)Instantiate(Impact, transform.position, transform.rotation);
            Destroy(effectIns, 0.2f);
        }
        if (col.gameObject.tag == "Bad_Bullet_Laser")
        {
            Destroy(gameObject);
        }
    }
}
