using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //public GameObject player;

    public Transform target;
    public Vector3 offset = new Vector3(3, 1, -20);
    public float damping = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 movePosition;

    void Start() {
        target = GameObject.Find("Player").transform;
    }

    void FixedUpdate() {
        movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
