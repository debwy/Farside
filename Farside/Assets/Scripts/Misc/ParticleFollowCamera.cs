using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowCamera : MonoBehaviour
{

    private float startX;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        startX = transform.position.x;
    }

    private float distX;

    void FixedUpdate()
    {
        //how far we have moved in world space
        distX = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startX + distX, transform.position.y, transform.position.z);
    }
}
