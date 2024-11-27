using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrampusController : MonoBehaviour
{
    public Vector2 teleportRange = Vector2.zero;
    public GameObject TeleportEffect;

    private Vector3 nextPos = Vector3.zero;

    private void Start()
    {
        nextPos = new Vector2(Random.Range(-teleportRange.x, teleportRange.x), Random.Range(-teleportRange.y, teleportRange.y));
        CreateEffect(nextPos);

        InvokeRepeating("Teleport", 5, 5);
    }

    void Teleport()
    {
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
