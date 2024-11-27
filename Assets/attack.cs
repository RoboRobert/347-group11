using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class attack : MonoBehaviour
{

    [Header("Set Dynamically")]
    public float speed = 40f;
    public float health = 100f;

    private Rigidbody2D attack_rb;

    public int lifetime = 30;
    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        attack_rb = GetComponent<Rigidbody2D>();

    }

    public void attack_movement_func()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        attack_movement_func();
        
    }

    void FixedUpdate()
    {
        if (lifetime < 0)
        {
            Destroy(this.gameObject);
        }

        lifetime -= 1;

    }

}
