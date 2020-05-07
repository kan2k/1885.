using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneBehavior : MonoBehaviour
{
    public Rigidbody2D DroneRB;
    public LineRenderer Drone;
    public float fireCountdown = 1f;
    public float fireRate;
    public Transform DronefirePoint;
    public GameObject bulletPrefab;
    public Transform CharacterPos;

    void Start()
    {
        // Set Color base on Settings
        Color32 playcolor1 = settingsBehavior.playcolor1;
        Color32 playcolor2 = settingsBehavior.playcolor2;
        Drone.startColor = playcolor1;
        Drone.endColor = playcolor2;
    }
    void Update()
    {
        Drone.startColor = settingsBehavior.playcolor1;
        Drone.endColor = settingsBehavior.playcolor2;
        fireRate = characterBehavior.fireRate * 0.5f;
        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, DronefirePoint.position, DronefirePoint.rotation);
    }
}
