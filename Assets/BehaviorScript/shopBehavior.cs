using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shopBehavior : MonoBehaviour
{
    public TextMeshProUGUI goldTextWhite;
    public TextMeshProUGUI goldTextBlack;
    public static float gold = 0;
    public static bool isShopOpened = false;
    public GameObject pauseMenu;
    public GameObject shopMenu;

    public Animator Heal10HP;
    public Animator HealthPoints;
    public Animator Speed;
    public Animator FireRate;
    public Animator FirePoints;
    public Animator Drone;
    public Animator HealDrone;
    public Animator NewWeapon;
    public Animator BackText;
    public TextMeshProUGUI Heal10HPText;
    public TextMeshProUGUI HealthPointsText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI FireRateText;
    public TextMeshProUGUI FirePointsText;
    public TextMeshProUGUI DroneText;
    public TextMeshProUGUI HealDroneText;
    public TextMeshProUGUI NewWeaponText;
    public float Heal10HPPrice;
    public float HealthPointsPrice;
    public float SpeedPrice;
    public float FireRatePrice;
    public float FirePointsPrice;
    public float DronePrice;
    public float HealDronePrice;
    public float NewWeaponPrice;
    public int Heal10HPLevel;
    public int HealthPointsLevel;
    public int SpeedLevel;
    public int FireRateLevel;
    public int FirePointsLevel;
    public int DroneLevel;
    public int HealDroneLevel;
    public int NewWeaponLevel;

    public GameObject dronePrefab;
    public GameObject healdronePrefab;
    public Transform characterTransform;
    public TextMeshProUGUI CenterText;
    public Animator CenterTextAnimator;

    public AudioSource UpgradeAudio;
    public AudioSource SelectAudio;
    public AudioSource PressAudio;

    private int index;
    public int totalUpgrades;

    void Start()
    {
        index = 0;
        totalUpgrades = 9;
        Heal10HPPrice = 10f;
        HealthPointsPrice = 10f;
        SpeedPrice = 5f;
        FireRatePrice = 10f;
        FirePointsPrice = 100f;
        DronePrice = 150f;
        HealDronePrice = 150f;
        NewWeaponPrice = 100f;

        Heal10HPLevel = 0;
        HealthPointsLevel = 0;
        SpeedLevel = 0;
        FireRateLevel = 0;
        FirePointsLevel = 0;
        DroneLevel = 0;
        HealDroneLevel = 0;
        NewWeaponLevel = 0;
    }

    void Update()
    {
        // Add, Update Gold
        if (!pauseBehavior.isPaused && !characterBehavior.isDead)
        {
            goldTextWhite.text = Mathf.Round(gold).ToString();
            goldTextBlack.text = goldTextWhite.text;
            gold += Time.deltaTime * 1;
        }

        // Open Menu 
        if ((Input.GetKeyDown(KeyCode.E)) && !pauseBehavior.isPauseMenuOpened && !characterBehavior.isDead)
        {
            if (isShopOpened) {
                Resume();
            } else {
                isShopOpened = true;
                OpenShop();
                index = 0;
            }
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isShopOpened && !pauseBehavior.isPauseMenuOpened)
        {
            if (index > 0)
            {
                index--;
                SelectAudio.Play();
            }
        }

        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && isShopOpened && !pauseBehavior.isPauseMenuOpened)
        {
            if (index < totalUpgrades - 1)
            {
                index++;
                SelectAudio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && isShopOpened && !pauseBehavior.isPauseMenuOpened)
        {
            if (index == 0)
            {;
                UpgradeHeal10HP();
            }
            else
            if (index == 1)
            {
                UpgradeHealthPoint();
            }
            else
            if (index == 2)
            {
                UpgradeSpeed();
            }
            else
            if (index == 3)
            {
                UpgradeFireRate();
            }
            else
            if (index == 4)
            {
                UpgradeFirePoints();
            }
            else
            if (index == 5)
            {
                UpgradeDrone();
            }
            else
            if (index == 6)
            {
                UpgradeHealDrone();
            }
            else
            if (index == 7)
            {
                UpgradeNewWeapon();
            }
            else
            if (index == 8)
            {
                PressAudio.Play();
                Resume();
            }
            else
                Debug.LogError("Index Broken (ShopBehavior)");
        }

        if (isShopOpened)
        {
            if (index == 0)
            {
                Heal10HP.Play("UpgradeAnimation");
            }
            else { Heal10HP.Play("NoAnimation"); }
            if (index == 1)
            {
                HealthPoints.Play("UpgradeAnimation");
            }
            else { HealthPoints.Play("NoAnimation"); }
            if (index == 2)
            {
                Speed.Play("UpgradeAnimation");
            }
            else { Speed.Play("NoAnimation"); }
            if (index == 3)
            {
                FireRate.Play("UpgradeAnimation");
            }
            else { FireRate.Play("NoAnimation"); }
            if (index == 4)
            {
                FirePoints.Play("UpgradeAnimation");
            }
            else { FirePoints.Play("NoAnimation"); }
            if (index == 5)
            {
                Drone.Play("UpgradeAnimation");
            }
            else { Drone.Play("NoAnimation"); }
            if (index == 6)
            {
                HealDrone.Play("UpgradeAnimation");
            }
            else { HealDrone.Play("NoAnimation"); }
            if (index == 7)
            {
                NewWeapon.Play("UpgradeAnimation");
            }
            else { NewWeapon.Play("NoAnimation"); }
            if (index == 8)
            {
                BackText.Play("menuTextAnimation");
            }
            else { BackText.Play("NoAnimation"); }
        }
    }
 
    void OpenShop()
    {
        pauseMenu.SetActive(false);
        shopMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseBehavior.isPaused = true;
        intAllShopText();
    }
    void Resume()
    {
        isShopOpened = false;
        shopMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseBehavior.isPaused = false;
    }

    void UpdateGold()
    {
        goldTextWhite.text = "Gold: " + Mathf.Round(gold).ToString();
        goldTextBlack.text = goldTextWhite.text;
    }

    void intAllShopText()
    {
        Heal10HPText.text = "10 Gold";
        HealthPointsText.text = "Lv " + HealthPointsLevel + " : " + (HealthPointsPrice + (Mathf.Round(characterBehavior.maxhealthPoints) - 100)) + " Gold";
        SpeedText.text = "Lv " + SpeedLevel + " : " + (SpeedPrice + (characterBehavior.maxSpeed - 2)) + " Gold";
        FireRateText.text = "Lv " + FireRateLevel + " : " + (FireRatePrice + (characterBehavior.fireRate - 5) * 10) + " Gold";
        FirePointsText.text = "Lv " + FirePointsLevel + " : " + ((1 + FirePointsLevel) * 100) + " Gold";
        DroneText.text = "Lv " + DroneLevel + " : " + ((1 + DroneLevel) * 100) + " Gold";
        HealDroneText.text = "Lv " + HealDroneLevel + " : " + ((1 + HealDroneLevel) * 100) + " Gold";
        NewWeaponText.text = "Lv " + NewWeaponLevel + " : " + ((1 + NewWeaponLevel) * 1000) + " Gold";
    }

    public void UpgradeHeal10HP()
    {
        if (gold < Heal10HPPrice)
        {
            Heal10HPText.text = "Not Enough Money!";
        }
        else
        if (characterBehavior.currenthealthPoints + 10 >= characterBehavior.maxhealthPoints)
        {
            Heal10HPText.text = "You can't be healed";
        }
        else
        if (gold >= Heal10HPPrice)
        {
            gold -= Heal10HPPrice;
            characterBehavior.currenthealthPoints += 10;
            Heal10HPText.text = "Healed to: " + Mathf.Round(characterBehavior.currenthealthPoints);
            UpdateGold();
            UpgradeAudio.Play();
        }
    }
    public void UpgradeHealthPoint()
    {
        if (characterBehavior.maxhealthPoints >= 200)
        {
            HealthPointsText.text = "You're maxed!";
        }
        else
        if (gold < (HealthPointsPrice + (characterBehavior.maxhealthPoints - 100)))
        {
            HealthPointsText.text = "You Need " + (HealthPointsPrice + (characterBehavior.maxhealthPoints - 100)) + " Gold";
        }
        else
        if (gold >= (HealthPointsPrice + (characterBehavior.maxhealthPoints - 100)))
        {
            gold -= (HealthPointsPrice + (characterBehavior.maxhealthPoints - 100));
            characterBehavior.maxhealthPoints += 10;
            characterBehavior.currenthealthPoints += 10;
            HealthPointsLevel += 1;
            HealthPointsText.text = "Upgraded to Lv " + HealthPointsLevel;
            UpdateGold();
            UpgradeAudio.Play();
        }
    }

    public void UpgradeSpeed()
    {
        if (characterBehavior.maxSpeed >= 12)
        {
            SpeedText.text = "You're maxed!";
        }
        else
        if (gold < (SpeedPrice + (characterBehavior.maxSpeed - 2)))
        {
            SpeedText.text = "You Need " + (SpeedPrice + (characterBehavior.maxSpeed - 2)) + " Gold";
        }
        else
        if (gold >= (SpeedPrice + (characterBehavior.maxSpeed - 2)))
        {
            gold -= (SpeedPrice + (characterBehavior.maxSpeed - 2));
            SpeedLevel += 1;
            characterBehavior.maxSpeed += 2;
            SpeedText.text = "Upgraded to Lv " + SpeedLevel;
            UpdateGold();
            UpgradeAudio.Play();
        }
    }

    public void UpgradeFireRate()
    {
        if (characterBehavior.fireRate  >= 10)
        {
            FireRateText.text = "You're maxed!";
        } else
        if (gold < (FireRatePrice + (characterBehavior.fireRate - 5) * 10))
        {
            FireRateText.text = "You Need " + ((characterBehavior.fireRate - 5) * 10) + " Gold";
        } else
        if (gold >= (FireRatePrice + (characterBehavior.fireRate - 5) * 10))
        {
            gold -= (FireRatePrice + (characterBehavior.fireRate - 5) * 10);
            characterBehavior.fireRate += 0.5f;
            FireRateLevel += 1;
            FireRateText.text = "Upgraded to Lv " + FireRateLevel;
            UpdateGold();
            UpgradeAudio.Play();
        }
    }

    public void UpgradeFirePoints()
    {
        if (characterBehavior.FirePointUnlockLevel >= 5)
        {
            FirePointsText.text = "You're maxed!";
        } else
        if (gold < ((1 + FirePointsLevel) * 100))
        {
            FirePointsText.text = "You Need " + ((1 + FirePointsLevel) * 100) + " Gold";
        } else
        if (gold >= ((1 + FirePointsLevel) * 100))
        {
            gold -= ((1 + FirePointsLevel) * 100);
            characterBehavior.FirePointUnlockLevel += 1;
            FirePointsLevel += 1;
            FirePointsText.text = "Upgraded to Lv " + FirePointsLevel;
            UpdateGold();
            UpgradeAudio.Play();
        }
    }

    public void UpgradeDrone()
    {
        if (characterBehavior.DroneUnlockLevel >= 6)
        {
            DroneText.text = "You're maxed!";
        } else
        if (gold < ((1 + DroneLevel) * 100))
        {
            DroneText.text = "You Need " + ((1 + DroneLevel) * 100) + " Gold";
        } else
        if (gold >= ((1 + DroneLevel) * 100))
        {
            gold -= ((1 + DroneLevel) * 100);
            characterBehavior.DroneUnlockLevel += 1;
            DroneLevel += 1;
            DroneText.text = "Upgraded to Lv " + DroneLevel;
            AddDrone();
            UpdateGold();
            UpgradeAudio.Play();
        }
    }
    public void UpgradeHealDrone()
    {
        if (characterBehavior.HealDroneUnlockLevel >= 2)
        {
            HealDroneText.text = "You're maxed!";
        } else
        if (gold < ((1 + HealDroneLevel) * 100))
        {
            HealDroneText.text = "You Need " + ((1 + HealDroneLevel) * 100) + " Gold";
        } else
        if (gold >= ((1 + HealDroneLevel) * 100))
        {
            gold -= ((1 + HealDroneLevel) * 100);
            characterBehavior.HealDroneUnlockLevel += 1;
            HealDroneLevel += 1;
            HealDroneText.text = "Upgraded to Lv " + HealDroneLevel;
            AddHealDrone();
            UpdateGold();
            UpgradeAudio.Play();
        }
    }

    void UpgradeNewWeapon()
    {
        if (characterBehavior.NewWeaponUnlockLevel >= 1)
        {
            NewWeaponText.text = "You're maxed!";
        }
        else
        if (gold < ((1 + NewWeaponLevel) * 1000))
        {
            NewWeaponText.text = "You Need " + ((1 + NewWeaponLevel) * 1000) + " Gold";
        }
        else
        if (gold >= ((1 + NewWeaponLevel) * 1000))
        {
            gold -= ((1 + NewWeaponLevel) * 1000);
            characterBehavior.NewWeaponUnlockLevel += 1;
            NewWeaponLevel += 1;
            NewWeaponText.text = "Upgraded to Lv " + NewWeaponLevel;
            UpdateGold();
            UpgradeAudio.Play();
            NewWeaponMessage();
        }
    }
    void AddDrone()
    {
        if (characterBehavior.DroneUnlockLevel == 1)
            Instantiate(dronePrefab, new Vector3(characterTransform.position.x + 0.7f, characterTransform.position.y - 0.1f, 0f), characterTransform.rotation, characterTransform);
        if (characterBehavior.DroneUnlockLevel == 2)
            Instantiate(dronePrefab, new Vector3(characterTransform.position.x - 0.4f, characterTransform.position.y - 0.1f, 0f), characterTransform.rotation, characterTransform);
        if (characterBehavior.DroneUnlockLevel == 3)
            Instantiate(dronePrefab, new Vector3(characterTransform.position.x + 1.1f, characterTransform.position.y -0.1f, 0f), characterTransform.rotation, characterTransform);
        if (characterBehavior.DroneUnlockLevel == 4)
            Instantiate(dronePrefab, new Vector3(characterTransform.position.x - 0.8f, characterTransform.position.y -0.1f, 0f), characterTransform.rotation, characterTransform);
        if (characterBehavior.DroneUnlockLevel == 5)
            Instantiate(dronePrefab, new Vector3(characterTransform.position.x + 1.5f, characterTransform.position.y-0.1f, 0f), characterTransform.rotation, characterTransform);
        if (characterBehavior.DroneUnlockLevel == 6)
            Instantiate(dronePrefab, new Vector3(characterTransform.position.x -1.2f, characterTransform.position.y-0.1f, 0f), characterTransform.rotation, characterTransform);
    }
    void AddHealDrone()
    {
        if (characterBehavior.HealDroneUnlockLevel == 1)
            Instantiate(healdronePrefab, new Vector3(characterTransform.position.x + 0.5f, characterTransform.position.y - 0.3f, 0f), characterTransform.rotation, characterTransform);
        if (characterBehavior.HealDroneUnlockLevel == 2)
            Instantiate(healdronePrefab, new Vector3(characterTransform.position.x - 0.2f, characterTransform.position.y - 0.3f, 0f), characterTransform.rotation, characterTransform);
    }

    void NewWeaponMessage()
    {
        CenterText.text = "TAB to Switch";
        CenterTextAnimator.Play("FadeOutAnimation");
    }
}
