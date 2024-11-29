using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject spawnEffect;
    public Vector2 spawnRange = Vector2.zero;
    public int spawnCount = 1;

    private bool explored = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Detects whether a player has entered the room
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collidedWith = other.gameObject; // what we collided with ?

        Debug.Log("TRIGGER!");

        if (!collidedWith.CompareTag("Player"))
        {
            return;
        }
        if(!explored)
        {
            StartCoroutine(SpawnEnemy());
        }

        explored = true;
    }

    //Spawns an enemy with the room as its parent within the specified spawn range
    IEnumerator SpawnEnemy()
    {
        Vector3 posVector = new Vector3(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));
        GameObject effect = Instantiate(spawnEffect, this.transform);
        effect.transform.localPosition = posVector;
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(2);
        Destroy(effect);
        GameObject newEnemy = Instantiate(enemyPrefab, this.transform);
        newEnemy.transform.localPosition = posVector;
    }
}
