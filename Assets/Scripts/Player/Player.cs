using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class Player : MonoBehaviour
{
    [Header("Set Dynamically")]
    public float speed = 3f;
    public float health = 100f;

    private Rigidbody2D _rb;

    public Text healthGT;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        _rb = GetComponent<Rigidbody2D>();
        
        // Set the camera to follow the current gameObject
        FollowCam.POI = gameObject;


        // Get the health text game object
        GameObject healthGO = GameObject.Find("HealthText");
        healthGT = healthGO.GetComponent<Text>();
        healthGT.text = "Health: ";

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

        string hearts = "";
        float temp = health;
        while (temp > 0) 
        {
            if (temp%20 == 0) 
            {
                hearts += "<";
            }
            else
            {
                hearts += "3";
            }
            temp -= 10;
        }

        

        healthGT.text = "Health: " + hearts;


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
