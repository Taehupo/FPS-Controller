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

    float spawnRateTimer = 9999999.9f;

    //Flags
    [SerializeField]
    bool isTimedSpawner = false;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimedSpawner)
        {
            if (spawnRateTimer >= enemySpawnRate)
            {
                Spawn();
                spawnRateTimer = 0.0f;
            }
            spawnRateTimer += Time.deltaTime;
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
