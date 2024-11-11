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
    public float followDistance = 10f;
    
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponentInParent<Rigidbody>();
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
        // Gets a normalized distance to the target.
        Vector3 targetDir = target.transform.position - _rb.transform.position;
        if (targetDir.magnitude < followDistance)
            return;
        _rb.velocity = targetDir.normalized * (speed * Time.fixedDeltaTime);
    }
}
