using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog : MonoBehaviour
{
    [Header("Set Dynamically")]
    public GameObject target;
    public float speed = 20f;

    //Follow distance in grid squares of 32 pixels each
    public float followDistance = 2f;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        
        //Calls the movement logic for the enemy
        movement_func();
    }

    public void make_bigger()
    {
        Vector3 currentsize = this.transform.localScale;
        this.transform.localScale = currentsize * 1.2f;
    }

    // Enemy movement logic
    void movement_func()
    {
        //Amount to move the current enemy toward the target
        Vector3 movementVector = Vector3.zero;
        // Gets a normalized distance to the target.
        Vector3 targetDir = target.transform.position - _rb.transform.position;

        //If the distance to the target is large enough, move toward the target
        if (targetDir.magnitude > followDistance * 0.32)
        {
            movementVector = targetDir.normalized * (speed * Time.fixedDeltaTime);
            //Apply the movement vector
            _rb.velocity = movementVector;
        }

        // if the distance is too crazy, teleport
        else if (targetDir.magnitude > 1.25)
        {
            Vector3 newposition = target.transform.position;
            newposition.y = targetDir.y + .05f;
            _rb.transform.position = newposition;
        }

        
    }
}
