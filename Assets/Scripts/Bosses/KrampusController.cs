using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrampusController : MonoBehaviour
{
    public Vector2 teleportRange = Vector2.zero;
    public GameObject TeleportEffect;

    public bool dead = false;

    private Vector3 nextPos = Vector3.zero;

    private StatManager stats;

    private void Start()
    {
        stats = GetComponentInParent<StatManager>();
        nextPos = new Vector2(Random.Range(-teleportRange.x, teleportRange.x), Random.Range(-teleportRange.y, teleportRange.y));
        CreateEffect(nextPos);

        InvokeRepeating("Teleport", 5, 5);
    }

    private void Update()
    {
        dead = stats.dead;

        if(dead)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponentInParent<StatManager>().dead = true;
        }
            
    }

    void Teleport()
    {
        if(dead) return;
        transform.localPosition = nextPos;
        nextPos = new Vector2(Random.Range(-teleportRange.x, teleportRange.x), Random.Range(-teleportRange.y, teleportRange.y));
        CreateEffect(nextPos);
    }

    void CreateEffect(Vector2 pos)
    {
        GameObject effect = Instantiate(TeleportEffect, transform.parent);
        effect.transform.localPosition = pos;
    }
}
