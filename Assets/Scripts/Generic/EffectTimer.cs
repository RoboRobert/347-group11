using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EffectTimer : MonoBehaviour
{
    public float lifetime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillSelf", lifetime);
    }

    void KillSelf()
    {
        Destroy(gameObject);
    }
}
