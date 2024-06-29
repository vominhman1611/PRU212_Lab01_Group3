using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyGO;
    public float maxSpawnRateInSecond = 1f;
    // Start is called before the first frame update 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        //this is the bottom-left point of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //this is the top-right point of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //instantate an enemy
        GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
        anEnemy.transform.position = new Vector2(Random.Range(min.x, max.y), max.y);

        //Schedule when to spawn next enemy
        ScheduleNextEnemySpawn();
    }
    void ScheduleNextEnemySpawn()
    {
        float spawnInNSecond;
        if (maxSpawnRateInSecond > 1f)
        {
            //pick a number between 1 and maxSpawnRateInSecond
            spawnInNSecond = Random.Range(1f, maxSpawnRateInSecond);
        }
        else
        {
            spawnInNSecond = 1f;
        }
        Invoke("SpawnEnemy", spawnInNSecond);
    }
    //function to increase the difficulty of the game 
    void IncreaseSpawnRate()
    {
            if(maxSpawnRateInSecond > 1f)
            {
                maxSpawnRateInSecond--;
            }
            if(maxSpawnRateInSecond == 1f)
            {
                CancelInvoke("IncreaseSpawnRate");
            }
    }
    public void ScheduleEnemySpawner()
    {
        //reset max spawn rate 
        float maxSpawnRateInSecond = 5f;

        Invoke("SpawnEnemy", maxSpawnRateInSecond);
        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }
    //Function to stop enemy spawner
    public void UnScheduleEnemySpawner()
    {
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncreaseSpawnRate");
    }
}
