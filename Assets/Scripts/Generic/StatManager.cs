using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    public float health = 100f;

    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0) {
            dead = true;
            transform.GetComponentInParent<EnemySpawner>().numDead += 1;
        }
    }
}
