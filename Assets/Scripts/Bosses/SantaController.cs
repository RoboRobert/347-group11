using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaController : MonoBehaviour
{
    [Header("Set Dynamically")]
    public float speed = 3f;

    //Follow distance in grid squares of 32 pixels each
    public float followDistance = 2f;

    private Rigidbody2D _rb;
    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.FindWithTag("Player");
    }

    void FixedUpdate()
    {
        if (GetComponentInParent<StatManager>().dead)
        {
            return;
        }
        //Calls the movement logic for the enemy
    }
}
