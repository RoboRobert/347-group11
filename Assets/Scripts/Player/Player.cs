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
    
    public float attack_size = 1f;
    public float attack_speed = 1f;
    public float attack_range = 1f;

    public GameObject attackPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        _rb = GetComponent<Rigidbody2D>();

        // Get the health text game object
        GameObject healthGO = GameObject.Find("HealthText");
        healthGT = healthGO.GetComponent<Text>();
        healthGT.text = "Health: ";

    }

    void Update()
    {
        // only call attack func if there was a Horizontal1 or Vertical1 keyboard event
        if (Input.GetKeyDown("up") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            Vector3 attack_vector = new Vector3(speed * Time.deltaTime, 0, 0);
           
            if (Input.GetKeyDown("up"))
            {
                attack_vector = Vector3.up /5;
            }
            else if (Input.GetKeyDown("down"))
            {
                attack_vector = Vector3.down /5;
            }
            else if (Input.GetKeyDown("left"))
            {
                attack_vector = Vector3.left /5;
            }
            else if (Input.GetKeyDown("right"))
            {
                attack_vector = Vector3.right /5;
            }

            // create a new Attack object
            GameObject pow = Instantiate<GameObject>(attackPrefab);    // instantiate a new object type GameObject with prefab attackPrefab
            pow.transform.position = transform.position;              // give the attack a transformation


            // call the attack object initialization func
            pow.GetComponent<attack>().attack_init_func(attack_size, attack_speed, attack_range);

            // call the attack object movement func:
            pow.GetComponent<attack>().attack_movement_func(attack_vector);


        }
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

        if (collidedWith.CompareTag("Enemy") || collidedWith.CompareTag("EnemyAttack"))
        {
            // player health goes down by 10
            health -= 10;
        }
        if (collidedWith.CompareTag("Weapon"))
        {
            Destroy(collidedWith);
            weaponType = coll.gameObject.name;
            UnityEngine.Debug.Log("Weapon collected: " + weaponType);

            if (weaponType == "Lobster")
            {
                attack_range = 50f;
                attack_size = .5f;
            }
            if (weaponType == "Hammer")
            {
                attack_range = 3f;
                attack_size = 1.5f;
            }

        }
        if (collidedWith.CompareTag("Life"))
        {
            Destroy(collidedWith);
            health += 10;
        }
        if (collidedWith.CompareTag("Food"))
        {
            Destroy(collidedWith);
            GameObject dog_obj = GameObject.FindWithTag("Dog");
            dog_obj.GetComponent<dog>().make_bigger();
        }
    }

}
