using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    public int lifetime = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime < 0) 
        {
            Destroy(this.gameObject);
        }

        lifetime -= 1;
        
    }
}
