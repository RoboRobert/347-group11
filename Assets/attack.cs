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
    public float damage = 10f;

    private Rigidbody2D attack_rb;

    public float lifetime = 1f;

    public float attack_size = 1f;

    // inverse proportional bullet speed, set by weapons in Player.cs
    public float bullet_speed = 10f;

    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        attack_rb = GetComponent<Rigidbody2D>();

        Invoke("KillSelf", lifetime);
    }

    public void attack_movement_func(Vector3 my_vector)
    {       

        direction = my_vector / bullet_speed;
        transform.Translate(my_vector);
    }

    public void attack_init_func(float size, float speed, float range)
    {
        bullet_speed= speed;
        lifetime = range;
        attack_size= size;

        this.transform.localScale = Vector3.one * size;
    }

    void FixedUpdate()
    {
        attack_movement_func(direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("Triggered!");
        GameObject other = collision.gameObject;
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<StatManager>().TakeDamage(damage);
        }
    }

    void KillSelf()
    {
        Destroy(gameObject);
    }
}
