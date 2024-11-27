using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnEffect;
    public Vector2 spawnRange = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 5);
    }

    //Spawns an enemy with the room as its parent within the specified spawn range
    void SpawnEnemy()
    {
        Vector3 posVector = new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));
        GameObject newEnemy = Instantiate(enemyPrefab, this.transform);
        newEnemy.transform.localPosition = posVector;
    }
}
