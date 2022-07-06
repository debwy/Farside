using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed;
    public int startingPoint;
    public Transform[] points;
    public bool isVerticalPlatform = true;

    [SerializeField]
    private Collider2D col;
    [SerializeField]
    private Collider2D platform;
    [SerializeField]
    private LayerMask thingsLayer;
    private int i = 0; //used for indexing array
    private bool squeezeState = false;

    void Start()
    {
        //sets platform to the location first point
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f) { //reaching destination platform
            ChangeDestination();
            squeezeState = false;
        } else if (!squeezeState && IsSqueeze()) {
            //starting point should be the higher one
            i = 0;
            squeezeState = true;
        }
    }

    private void ChangeDestination() {
        i++; //i points to next platform
            if (i == points.Length) {
                //if i points towards last destination, set i back to start
                i = 0;
        }
    }

    private bool IsSqueeze() {
        return col.IsTouchingLayers(thingsLayer) && isVerticalPlatform;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed);
    }

    private void OnCollisionEnter2D(Collision2D hit) {
        if (hit.gameObject.CompareTag("Player") && !IsSqueeze()) {
            hit.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D hit) {
        if (hit != null && hit.gameObject.CompareTag("Player")) {
            hit.transform.SetParent(null);
        }
    }
}
