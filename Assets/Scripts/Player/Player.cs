using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class Player : MonoBehaviour
{
    [Header("Set Dynamically")]
    public float speed = 3f;
    public float health = 100f;

    private Rigidbody2D _rb;

    public Text healthGT;

    public string weaponType = "None";

    // weapon default
    public float range = 2f;
    public float damage = 4f;

    public GameObject attackPrefab;


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

        // only call attack func if there was a Horizontal1 or Vertical1 keyboard event
        if (Input.GetKeyDown("up"))
        {
            attack_func();
            
        }
        

    }
    
    // funcion that controls attack logic
    void attack_func()
    {
        // create a new Attack object
        GameObject attack = Instantiate<GameObject>(attackPrefab);    // instantiate a new object type GameObject with prefab attackPrefab
        attack.transform.position = transform.position;              // give the attack a transformation

        // Normalize input vector so the player doesn't move faster diagonally
        //Vector3 velocityDir = new Vector3(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"), 0).normalized;
        //_rb.velocity = velocityDir * (speed * Time.fixedDeltaTime);
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
            // player health goes down by 10
            health -= 10;
        }
        if (collidedWith.CompareTag("Weapon"))
        {
            Destroy(collidedWith);
            weaponType = coll.gameObject.name;
            UnityEngine.Debug.Log("Weapon collected: " + weaponType);

        }
        if (collidedWith.CompareTag("Life"))
        {
            Destroy(collidedWith);
            health += 10;

        }

    }

}
