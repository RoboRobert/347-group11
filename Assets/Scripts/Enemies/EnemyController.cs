using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [Header("Set Dynamically")]
    public GameObject target;
    public float health = 100f;
    public float speed = 3f;
    
    //Follow distance in grid squares of 32 pixels each
    public float followDistance = 2f;
    
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    void FixedUpdate()
    {  
        //Calls the movement logic for the enemy
        movement_func();
    }

    // Enemy movement logic
    void movement_func()
    {
        //Amount to move the current enemy toward the target
        Vector3 movementVector = Vector3.zero;
        // Gets a normalized distance to the target.
        Vector3 targetDir = target.transform.position - _rb.transform.position;
        
        //If the distance to the target is large enough, move toward the target
        if (targetDir.magnitude > followDistance*0.32)
        {
            movementVector = targetDir.normalized * (speed * Time.fixedDeltaTime);
        }
    
        //Apply the movement vector
        _rb.velocity = movementVector;
    }
}
