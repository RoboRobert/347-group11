using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehavior : MonoBehaviour
{

    [Header("Set Dynamically")]
    public GameObject target;
    public GameObject player;
    public float speed = 3f;

    public float followDistance = 2f;
    public float attackDistance = 5f;

    private Rigidbody2D _rb;
    public GameObject[] enemies;

    


        // Start is called before the first frame update
       void Start()
        {

          if (enemies == null)
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            _rb = GetComponent<Rigidbody2D>();
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //Calls the movement logic for the enemy
        movement_func();
    }

    // Enemy movement logic
    void movement_func()
    {

        //Gets target
        foreach (GameObject enemy in enemies)
        {
            Vector3 closestDir = enemy.transform.position - player.transform.position;
            Vector3 currentTarget = target.transform.position - player.transform.position;
            if (closestDir.magnitude < attackDistance * 0.32 && closestDir.magnitude < currentTarget.magnitude)
            {
                target = enemy;
            }
        }

        //Amount to move dog toward the target
        Vector3 movementVector = Vector3.zero;
        // Gets a normalized distance to the target.
        Vector3 targetDir = target.transform.position - _rb.transform.position;

        //If the distance to the target is large enough, move toward the target
        if (targetDir.magnitude > followDistance * 0.32)
        {
            movementVector = targetDir.normalized * (speed * Time.fixedDeltaTime);
        }

        //Apply the movement vector
        _rb.velocity = movementVector;
    }
}
