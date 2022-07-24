using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyPlatform : MonoBehaviour
{
    [SerializeField] private Collider2D col;
    [SerializeField] private Collider2D platform;
    [SerializeField] private LayerMask thingsLayer;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
