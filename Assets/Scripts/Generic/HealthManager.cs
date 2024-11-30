using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 0f;

    private float startHealth = 0f;
    private StatManager stats;
    
    // Start is called before the first frame update
    void Start()
    {
        stats = transform.GetComponentInParent<StatManager>();
        healthAmount = stats.health;
        startHealth = healthAmount;

        InvokeRepeating("Test", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.dead)
            gameObject.SetActive(false);
        healthAmount = stats.health;
        healthBar.fillAmount = healthAmount / startHealth;
    }

    void Test()
    {
        stats.TakeDamage(10);
    }
}
