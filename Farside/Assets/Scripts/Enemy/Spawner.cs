using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
