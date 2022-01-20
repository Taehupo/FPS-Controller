using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    [Range(2.0f, 20.0f)]
    float spawnRange;

    [SerializeField]
    int enemyNumber;

    [SerializeField]
    List<GameObject> enemyList;

    [SerializeField]
    [Range(5.0f, 120.0f)]
    float enemySpawnRate;

    [SerializeField]
    [Range(0.01f, 0.75f)]
    float enemyIntermittantSpawnGap;

    float spawnRateTimer = 9999999.9f;
    float intermittentSpawnTimer = 9999999.9f;

    int intermittentEnnemyCounter = 0;

    //Flags
    [SerializeField]
    bool isTimedSpawner = false;

    [SerializeField]
    bool isSpawnIntermittent = false;

    [SerializeField]
    bool isIntermittentSpawnRandomized = false;

    bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!isTimedSpawner)
        {
            if (isSpawnIntermittent)
            {
                StartIntermittentSpawning();
            }
            else
            {
                Spawn();
            }            
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimedSpawner)
        {            
            if (spawnRateTimer >= enemySpawnRate && !isSpawnIntermittent)
            {
                Spawn();
                spawnRateTimer = 0.0f;
            }
            spawnRateTimer += Time.deltaTime;
        }
        if (isSpawnIntermittent)
        {
            if (spawnRateTimer >= enemySpawnRate && !isSpawning && isTimedSpawner)
            {
                StartIntermittentSpawning();
            }
            if (isSpawning)
            {
                IntermittentSpawn();
            }
            intermittentSpawnTimer += Time.deltaTime;
        }
    }

    void StartIntermittentSpawning()
    {
        isSpawning = true;
        intermittentEnnemyCounter = 0;
    }

    void IntermittentSpawn()
    {
        if (intermittentEnnemyCounter < enemyNumber)
        {
            foreach (GameObject enemyGo in enemyList)
            {
                if (!isIntermittentSpawnRandomized)
                {
                    if (intermittentSpawnTimer >= enemyIntermittantSpawnGap)
                    {
                        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z + Random.Range(-spawnRange, spawnRange));
                        Instantiate(enemyGo, spawnPosition, Quaternion.identity);
                        intermittentSpawnTimer = 0.0f;
                        intermittentEnnemyCounter++;
                    }
                }
                else
                {
                    if (intermittentSpawnTimer >= enemyIntermittantSpawnGap)
                    {
                        Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z + Random.Range(-spawnRange, spawnRange));
                        Instantiate(enemyGo, spawnPosition, Quaternion.identity);
                        intermittentSpawnTimer = 0.0f;
                        enemyIntermittantSpawnGap = Random.Range(0.01f, 0.5f);
                        intermittentEnnemyCounter++;
                    }
                }
            }
        }
        else
        {
            isSpawning = false;
            if (isTimedSpawner)
            {
                spawnRateTimer = 0.0f;
            }
        }
    }

    void Spawn()
    {
        foreach (GameObject enemyGo in enemyList)
        {
            for (int i = 1; i <= enemyNumber; ++i)
            {
                  Vector3 spawnPosition = new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z + Random.Range(-spawnRange, spawnRange));
                  Instantiate(enemyGo, spawnPosition, Quaternion.identity);               
            }            
        }
    }
}
