using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthText : MonoBehaviour
{
    static public int health = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Text gt = this.GetComponent<Text>();
        gt.text = "Health: " + health;
    }
}
