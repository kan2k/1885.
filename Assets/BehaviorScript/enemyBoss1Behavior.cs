using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBoss1Behavior : MonoBehaviour
{

    public GameObject LaserbulletPrefab;
    public GameObject Boss1bulletPrefab;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;

    public Transform firePointLeft;
    public Transform firePointRight;
    public Transform firePointMiddle;
    public float fireCountdown;
    public float fireRate;
    public float healthPoints;
    public float bulletSpeed = 6f;
    public float maxHealthPoints;

    // HP Bar
    public Transform bar;

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
        fireRate = 1f;
        fireCountdown = 1f;
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
        healthPoints = 200 * centertext.stage;
        maxHealthPoints = 200 * centertext.stage;
    }

    void Update()
    {
        // Update Hp
        float updatehp = (healthPoints / maxHealthPoints);
        bar.localScale = new Vector3(updatehp, 1f);

        // Fire Rate
        if (fireCountdown <= 0)
        {
            
            int randomShoot = Random.Range(0, 4);
            if (randomShoot == 1)
            {
                SpawnEnemy();
            }
            if (randomShoot == 2)
            {
                Shoot180();
            }
            if (randomShoot == 3)
            {
                StartCoroutine(ShootLaser());
            }
            
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

        // Enemy AI
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, borderBOTTOMLEFT.position.x + 2f, borderTOPRIGHT.position.x -2f)
            + (movementPerSecond.x * Time.deltaTime),
            Mathf.Clamp(transform.position.y, borderBOTTOMLEFT.position.y + 8f, borderTOPRIGHT.position.y - 1f)
            + (movementPerSecond.y * Time.deltaTime));

        // Health = 0 -> DEAD
        if (healthPoints <= 0)
        {
            shopBehavior.gold += 5;
            Destroy(gameObject);
        }
    }

    IEnumerator ShootLaser()
    {
        float timePassed = 0;
        while (timePassed < 3)
        {
            Instantiate(LaserbulletPrefab, firePointLeft.position, firePointLeft.rotation);
            Instantiate(LaserbulletPrefab, firePointRight.position, firePointRight.rotation);
            timePassed += Time.deltaTime;

            yield return null;
        }
    }

    void Shoot180()
    {
        Instantiate(Boss1bulletPrefab, firePointMiddle.position, firePointMiddle.rotation);
        Instantiate(Boss1bulletPrefab, firePointMiddle.position, firePointMiddle.rotation *= Quaternion.Euler(0, 0, 20));
        Instantiate(Boss1bulletPrefab, firePointMiddle.position, firePointMiddle.rotation *= Quaternion.Euler(0, 0, -20));
        Instantiate(Boss1bulletPrefab, firePointRight.position, firePointRight.rotation);
        Instantiate(Boss1bulletPrefab, firePointRight.position, firePointRight.rotation *= Quaternion.Euler(0, 0, 20));
        Instantiate(Boss1bulletPrefab, firePointRight.position, firePointRight.rotation *= Quaternion.Euler(0, 0, -20));
        Instantiate(Boss1bulletPrefab, firePointLeft.position, firePointLeft.rotation);
        Instantiate(Boss1bulletPrefab, firePointLeft.position, firePointLeft.rotation *= Quaternion.Euler(0, 0, 20));
        Instantiate(Boss1bulletPrefab, firePointLeft.position, firePointLeft.rotation *= Quaternion.Euler(0, 0, -20));
    }

    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, 4);
        if (randomEnemy == 1)
        {
            Instantiate(Enemy1, firePointRight.position, firePointRight.rotation);
        }
        if (randomEnemy == 2)
        {
            Instantiate(Enemy2, firePointRight.position, firePointRight.rotation);
        }
        if (randomEnemy == 3)
        {
            Instantiate(Enemy3, firePointRight.position, firePointRight.rotation);
        }
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
            movementDirection = new Vector2(.5f, 0f);
        if (Mathf.Round(dir) == 2)
            movementDirection = new Vector2(-.5f, 0f);
        if (Mathf.Round(dir) == 3)
            movementDirection = new Vector2(0f, .5f);
        if (Mathf.Round(dir) == 4)
            movementDirection = new Vector2(0f, -.5f);
        movementPerSecond = movementDirection * characterVelocity;
    }
}
