using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyGenerate : MonoBehaviour
{
    public GameObject[] enemyPre;

    public Transform enemySpawnPos;

    private int maxEnemyCount = 20;

    private int currentEnemyCount = 0;

    private Ray enemySpawnRay;

    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyRandomSpawn();
        spawnPos = new Vector3(Random.Range(40, 60), 0, Random.Range(40, 60));
    }
    private void EnemyRandomSpawn()
    {
        StartCoroutine(InstantiateEnemy());
        return;
        
    }
    IEnumerator InstantiateEnemy()
    {         
        while(currentEnemyCount!=maxEnemyCount)
        {
            currentEnemyCount++;
            GameObject enemys = Instantiate(enemyPre[Random.Range(0, 19)], spawnPos, transform.rotation) as GameObject;
            yield return null;
        }                  
    }
}
