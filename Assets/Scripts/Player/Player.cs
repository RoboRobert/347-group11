using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [Header("Set Dynamically")]
    public GameObject player;
    
    public float speed = 3f;
    public Rigidbody rb;
    public Vector3 velocity;

    public float health = 100f;


    // Start is called before the first frame update
    void Start()
    {
        FollowCam.POI = player;
        // initialize rigid body to be the player's rigidbody
        rb = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            UnityEngine.Debug.Log("Game over!!!");
            SceneManager.LoadScene("Scenes/Game_Over_Splash");
        }
    }

    void FixedUpdate()
    {
        // Normalize input vector so the player doesn't move faster diagonally
        Vector3 velocityDir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        
        movement_func(velocityDir);
    }

    void movement_func(Vector3 velocityDir)
    {
        rb.velocity = velocityDir * (speed * Time.fixedDeltaTime);
        velocity = rb.velocity;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject; // what we collided with ?

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
