using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        if (enemy != null) {
            Instantiate(enemy, transform.position, transform.rotation);
        }
    }
}
