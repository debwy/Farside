using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    private float length, startX;
    private float height, startY;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        startX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startY = transform.position.y;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private float distX;
    private float distY;

    void FixedUpdate()
    {
        //how far we have moved in world space
        distX = (cam.transform.position.x * parallaxEffect);
        distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startX + distX, transform.position.y, transform.position.z);
    }
}
