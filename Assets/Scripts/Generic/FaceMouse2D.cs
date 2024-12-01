using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse2D : MonoBehaviour
{
    public GameObject bulletPrefab;

    private Camera mainCam;
    private Vector3 mousePos;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 difference = mousePos - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        // Shoot the gun
        if (Input.GetMouseButtonDown(0))
            ShootGun();
    }

    void ShootGun()
    {
        if (bulletPrefab != null)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, this.transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.forward*10;
        }
    }
}
