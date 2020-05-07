using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2Behavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireCountdown = 1f;
    public float fireRate = 1f;
    public float healthPoints = 10;
    public float bulletSpeed = 6f;

    // Enemy AI - Movement
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 2f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    //border
    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;

    void Start()
    {
        // Random HP and Size
        float random = Random.Range(0.3f, 0.6f);
        healthPoints = Mathf.Round(random + 5) * centertext.stage;
        transform.localScale = new Vector3(random, random, 0f);

        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

    void Update()
    {
        // Enemy AI
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, borderBOTTOMLEFT.position.x - 0.68f, borderTOPRIGHT.position.x - 0.63f)
            + (movementPerSecond.x * Time.deltaTime),
            Mathf.Clamp(transform.position.y, borderBOTTOMLEFT.position.y + 7f, borderTOPRIGHT.position.y + 1.5f)
            + (movementPerSecond.y * Time.deltaTime));

        // Health = 0 -> DEAD
        if (healthPoints <= 0)
        {
            shopBehavior.gold += 5;
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Good_Bullet")
        {
            healthPoints -= 1;
        }
    }

    void calcuateNewMovementVector()
    {

        // Jason Kwok: I have a intergalactic brain to code all this boissss
        // Triangle Enemy Move Pattern

        float dir;
        dir = Random.Range(0, 4.5f);
        if (Mathf.Round(dir) == 0)
            movementDirection = new Vector2(0f, 0f);
        if (Mathf.Round(dir) == 1)
            movementDirection = new Vector2(1f, 0f);
        if (Mathf.Round(dir) == 2)
            movementDirection = new Vector2(-1f, 0f);
        if (Mathf.Round(dir) == 3)
            movementDirection = new Vector2(0f, 1f);
        if (Mathf.Round(dir) == 4)
            movementDirection = new Vector2(0f, -1f);

        Shoot();
        movementPerSecond = movementDirection * characterVelocity;
    }
}
