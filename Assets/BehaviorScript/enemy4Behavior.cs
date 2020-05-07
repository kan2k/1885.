using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy4Behavior : MonoBehaviour
{
    public GameObject bulletObject;
    public GameObject bulletObject2;
    public Transform firePoint;
    private float lifetime;
    private float intlifetime;

    void Start()
    {
        lifetime = Random.Range(4, 6);
        intlifetime = lifetime;
        transform.position += new Vector3 ( 0f, Random.Range(-4f, 2f) ,0f);
        InvokeRepeating("Shoot", 0f,  0.01f);
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(bulletObject, firePoint.position, firePoint.rotation);
        Instantiate(bulletObject2, firePoint.position, firePoint.rotation *= Quaternion.Euler(0, 0, 180));
    }
}
