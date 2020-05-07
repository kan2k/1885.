using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class characterBehavior : MonoBehaviour
{
    public Material GlowMaterial;
    public LineRenderer character;
    public LineRenderer characterbullet;
    public LineRenderer characterbulletbomb;
    public LineRenderer characterbulletbomb_small;
    public Rigidbody2D rb;
    public static float maxhealthPoints = 100;
    public static float currenthealthPoints = 100;
    public static float fireRate = 5;
    public static float fireCountdown = 0;
    public static float maxSpeed = 2;
    public static float FirePointUnlockLevel = 1;
    public static float DroneUnlockLevel = 0;
    public static float HealDroneUnlockLevel = 0;
    public static float NewWeaponUnlockLevel = 0;
    public static bool rgbbullet = false;
    public int ShootMethod;

    public GameObject bulletPrefab;
    public GameObject bulletBombPrefab;
    public Transform firePoint;
    public Transform firePointLeft1;
    public Transform firePointLeft2;
    public Transform firePointRight1;
    public Transform firePointRight2;
    public GameObject Impact;
    public Transform center;

    public Transform borderTOPRIGHT;
    public Transform borderBOTTOMLEFT;

    public GameObject pauseMenu;
    public GameObject shopMenu;
    public GameObject GameOverMenu;

    public AudioClip ShootingAudio;
    public AudioMixer audioMixer;
    public AudioSource SelectAudio;

    public static bool isDead = false;

    void Start()
    {
        // Set Audio Volume
        float volume = PlayerPrefs.GetFloat("Volume", 0.2f);
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);

        // Game Over Retry Reset
        transform.localScale = new Vector3(0.4f, 0.4f);
        shopBehavior.gold = 0;
        characterBehavior.isDead = false;
        Time.timeScale = 1f;

        // Reset Upgrades
        maxhealthPoints = 100;
        currenthealthPoints = 100;
        fireRate = 5;
        fireCountdown = 0;
        maxSpeed = 5;
        FirePointUnlockLevel = 1;
        DroneUnlockLevel = 0;
        HealDroneUnlockLevel = 0;
        NewWeaponUnlockLevel = 0;
        ShootMethod = 1;

        // Set Color base on Settings
        Color32 playcolor1 = settingsBehavior.playcolor1;
        Color32 playcolor2 = settingsBehavior.playcolor2;
        character.startColor = playcolor1;
        character.endColor = playcolor2;
        characterbullet.startColor = playcolor1;
        characterbullet.endColor = playcolor2;
        characterbulletbomb.startColor = playcolor1;
        characterbulletbomb.endColor = playcolor1;
        characterbulletbomb_small.startColor = playcolor1;
        characterbulletbomb_small.endColor = playcolor1;
        float emission = Mathf.PingPong(Time.time, 0.1f);
        Color baseColor = playcolor1;
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        GlowMaterial.SetColor("_EmissionColor", finalColor);
    }

    void Update()
    {
        if (!pauseBehavior.isPaused && !isDead)
        {
            // Movement
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

            // Move to Play Area - Borders
            transform.position = new Vector3
            (
                Mathf.Clamp(transform.position.x + movement.x * Time.deltaTime * maxSpeed, borderBOTTOMLEFT.position.x - 0.68f, borderTOPRIGHT.position.x - 0.63f),
                Mathf.Clamp(transform.position.y + movement.y * Time.deltaTime * maxSpeed, borderBOTTOMLEFT.position.y + 2f, borderTOPRIGHT.position.y - 2f),
                0.0f
            );

            // Shoot Method Switching
            if (Input.GetKeyDown(KeyCode.Tab) && NewWeaponUnlockLevel == 1)
            {
                Debug.Log("switching");
                SwitchMethod();
            }

            // Shooting: Get Input & Call Function "Shoot" & Fire Rate Manager
            if ((Input.GetKey("space") || settingsBehavior.autoFireToggle) && fireCountdown <= 0 && ShootMethod == 1)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            if ((Input.GetKey("space") || settingsBehavior.autoFireToggle) && fireCountdown <= 0 && ShootMethod == 2)
            {
                ShootBomb();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;

            // Dead Funcution
            if (currenthealthPoints <= 0 || Input.GetKey(KeyCode.P))
            {
                isDead = true;
                Death();
            }

            // Add money
            if (Input.GetKey(KeyCode.O))
            {
                shopBehavior.gold += 100;
            }
        }

        if (isDead)
        {
            rb.velocity = new Vector2(0f, 0f);
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0f, 0.05f), Mathf.Lerp(transform.localScale.y, 0f, 0.05f), 0f);
        }


    }


    // Shooting: Function
    void Shoot()
    {
        if (FirePointUnlockLevel >= 1)
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (FirePointUnlockLevel >= 2)
            Instantiate(bulletPrefab, firePointLeft1.position, firePointLeft1.rotation);
        if (FirePointUnlockLevel >= 3)
            Instantiate(bulletPrefab, firePointRight1.position, firePointRight1.rotation);
        if (FirePointUnlockLevel >= 4)
            Instantiate(bulletPrefab, firePointLeft2.position, firePointLeft2.rotation);
        if (FirePointUnlockLevel >= 5)
            Instantiate(bulletPrefab, firePointRight2.position, firePointRight2.rotation);
    }

    void ShootBomb()
    {
        if (FirePointUnlockLevel >= 1)
            Instantiate(bulletBombPrefab, firePoint.position, firePoint.rotation);
        if (FirePointUnlockLevel >= 2)
            Instantiate(bulletBombPrefab, firePointLeft1.position, firePointLeft1.rotation);
        if (FirePointUnlockLevel >= 3)
            Instantiate(bulletBombPrefab, firePointRight1.position, firePointRight1.rotation);
        if (FirePointUnlockLevel >= 4)
            Instantiate(bulletBombPrefab, firePointLeft2.position, firePointLeft2.rotation);
        if (FirePointUnlockLevel >= 5)
            Instantiate(bulletBombPrefab, firePointRight2.position, firePointRight2.rotation);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bad_Bullet")
        {
            currenthealthPoints -= 10;
        }
        if (col.gameObject.tag == "Breakable_Bad_Bullet")
        {
            currenthealthPoints -= 20;
        }
        if (col.gameObject.tag == "Bad_Bullet_Laser")
        {
            currenthealthPoints -= 1f;
        }
    }
    void Death()
    {
        Time.timeScale = 0.1f;
        GameObject effectIns = (GameObject)Instantiate(Impact, center.position, center.rotation);
        Destroy(effectIns, 0.5f);
        Debug.Log("Game Over!");
        StartCoroutine(OpenGameOverMenu());
    }

    IEnumerator OpenGameOverMenu()
    {
        yield return new WaitForSeconds(0.1f);
        pauseMenu.SetActive(false);
        shopMenu.SetActive(false);
        GameOverMenu.SetActive(true);
    }

    /* IEnumerator MinusHealth(float inthp, float minushp)
    {
        float minus = currenthealthPoints - minushp;
        float elapsed = 0.0f;
        while (elapsed <= .5f)
        {
            currenthealthPoints = Mathf.Lerp(currenthealthPoints, minus, elapsed / .5f);
            elapsed += Time.deltaTime;
            yield return null;
            Mathf.Round(currenthealthPoints);
        }
        currenthealthPoints = minus;
    } */
    void SwitchMethod()
    {
        if (ShootMethod == 1)
        {
            ShootMethod = 2;
            SelectAudio.Play();
        } else
        if (ShootMethod == 2)
        {
            ShootMethod = 1;
            SelectAudio.Play();
        }
    }

}