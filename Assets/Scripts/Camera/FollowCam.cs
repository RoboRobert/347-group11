using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Most of this code comes from the  Mission Demolition follow cam in the textbook
    // create a point of interest game object
    static public GameObject POI;

    [Header("Set Dynamically")]
    public float camZ;

    void Awake ()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate ()
    {
        if (!POI)
        {
            return;
        }

        Vector3 destination = POI.transform.position;

        destination.z = camZ;

        transform.position = destination;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
