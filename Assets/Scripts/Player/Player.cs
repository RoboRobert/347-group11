using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

public class Player : MonoBehaviour
{
    [Header("Set Dynamically")]
    public float speed = 3f;
    public float health = 100f;

    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        _rb = GetComponent<Rigidbody2D>();
        
        // Set the camera to follow the current gameObject
        FollowCam.POI = gameObject;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            UnityEngine.Debug.Log("Game over!!!");
            SceneManager.LoadScene("Scenes/Game_Over_Splash");
        }
        
        //Calls the movement logic for the player
        movement_func();
    }
    
    // Player movement logic
    void movement_func()
    {
        // Normalize input vector so the player doesn't move faster diagonally
        Vector3 velocityDir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        
        _rb.velocity = velocityDir * (speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.gameObject; // what we collided with ?

        UnityEngine.Debug.Log("Collision!");

        if (collidedWith.CompareTag("Enemy"))
        {
            //Destroy(collidedWith);

            // TO DO: make score go down when collided with enemy 
            
            /*
            int score = int.Parse(scoreGT.text);
            score += 50;
            scoreGT.text = score.ToString();
            */

            health -= 10;
        }

    }

}
