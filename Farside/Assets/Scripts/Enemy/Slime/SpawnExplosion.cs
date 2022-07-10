using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExplosion : MonoBehaviour
{
    public Explosion boom;

    private Vector3 offset = new Vector3(0f, -2f, 0f);

    public void Explode()
    {
        if (boom != null) {
            Instantiate(boom, transform.position + offset, transform.rotation);
        } else {
            Explode();
        }
    }
}
