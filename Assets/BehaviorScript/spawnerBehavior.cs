using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawnerBehavior : MonoBehaviour
{

    public Transform enemy1Prefab;
    public Transform enemy2Prefab;
    public Transform enemy3Prefab;
    public Transform enemy4Prefab;
    public Transform enemyBoss1Prefab;
    public enum SpawnState { SPAWNING, WAITING, COUNTING };


    // Spawn Boss Audio
    public AudioClip bossSpawnAudio;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public int enemy1count;
        public int enemy2count;
        public int enemy3count;
        public int enemy4count;
        public int enemyBoss1count;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Transform[] spawnPoints;
    public Transform bossSpawn;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced.");
        }

        waveCountdown = 2;
    }

    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!bossIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    bool bossIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Boss") == null)
            {
                return false;
            }
        }
        return true;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.enemy1count; i++)
        {
            SpawnEnemy(enemy1Prefab);
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < _wave.enemy2count; i++)
        {
            SpawnEnemy(enemy2Prefab);
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < _wave.enemy3count; i++)
        {
            SpawnEnemy(enemy3Prefab);
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < _wave.enemy4count; i++)
        {
            SpawnEnemyLeftRight(enemy4Prefab);
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < _wave.enemyBoss1count; i++)
        {
            SpawnEnemyMiddle(enemyBoss1Prefab);
            yield return new WaitForSeconds(0.2f);
        }
        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, 3)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }

    void SpawnEnemyLeftRight(Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(3, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
    void SpawnEnemyMiddle(Transform _enemy)
    {
        Transform _sp = bossSpawn;
        Instantiate(_enemy, _sp.position, _sp.rotation);
        AudioSource.PlayClipAtPoint(bossSpawnAudio, transform.position);
    }


}