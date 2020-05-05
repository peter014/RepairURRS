using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemies;

    // Probability of spawned enemy to be a melee
    public float meleeProbability = 0.8f;


    // Time to spawn enemy
    public float spawnTime = 5.0f;

    // Last time of spawn
    public float previousSpawnTime = 0.0f;

    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime += Random.Range(0.0f, 2.5f);
        previousSpawnTime = spawnTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (previousSpawnTime >= spawnTime && USSRManager.Instance.enemyCount < USSRManager.Instance.enemyMax)
        {
            if (Random.Range(0.0f, 1.0f) <= meleeProbability)
            {
                GameObject enemy = Instantiate(enemies[0], spawnPoint.position,
                                spawnPoint.rotation);
                enemy.GetComponent<AIMeleeAttack>().damage =
                    enemy.GetComponent<AIMeleeAttack>().baseDamage * USSRManager.Instance.level;
                USSRManager.Instance.enemyCount++;
            }
            else
            {
                GameObject enemy = Instantiate(enemies[1], spawnPoint.position,
                                spawnPoint.rotation);
                enemy.GetComponent<AIShoot>().damage =
                    enemy.GetComponent<AIShoot>().baseDamage * USSRManager.Instance.level;
                USSRManager.Instance.enemyCount++;
            }
            previousSpawnTime = 0.0f;
        }
        else
        {
            previousSpawnTime += Time.deltaTime;
            
        }
        
    }
}
