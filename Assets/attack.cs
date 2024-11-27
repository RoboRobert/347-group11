using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;


public class attack : MonoBehaviour
{

    [Header("Set Dynamically")]
    public float speed = 10f;
    public float health = 100f;

    private Rigidbody2D attack_rb;

    public int lifetime = 2;

    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        attack_rb = GetComponent<Rigidbody2D>();

        

    }

    public void attack_movement_func(Vector3 my_vector)
    {
        direction= my_vector;
        transform.Translate(my_vector / 5);

    }

    // Update is called once per frame
    void Update()
    {
        lifetime = lifetime - 1;

        if (lifetime <= 0)
        {
            UnityEngine.Debug.Log("destroying attack");

            Destroy(this.gameObject);
        }

    }

    void FixedUpdate()
    {

        
        attack_movement_func(direction);

    }



}
