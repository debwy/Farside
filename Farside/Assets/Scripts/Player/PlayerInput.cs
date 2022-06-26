using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    internal PlayerMain player;

    // Start is called before the first frame update
    void Start()
    {

    }

    internal float moveHorizontal;
    internal float moveVertical;
    internal bool attacking;
    internal bool isJumping;
    internal bool shooting;
    internal bool dashing;

    internal float direction;

    // Update is called once per frame
    void Update()
    {
        Raycasting();
        CheckInput();
    }

    private void CheckInput() {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        attacking = Input.GetMouseButtonDown(0);
        shooting = Input.GetMouseButtonDown(1);
        isJumping = !Grounded();
        dashing = Input.GetKeyDown(KeyCode.LeftShift);
        
        if (player.faceRight) {
            direction = 1f;
        } else {
            direction = -1f;
        }
    }

    public GameObject groundRay;
    private RaycastHit2D hitGround;
    public GameObject groundRayAlt;
    private RaycastHit2D hitGroundAlt;
    public float groundedValue = 0.1f;

    public GameObject obstacleRay;
    private RaycastHit2D hitObs;
    public float wallValue = 0.3f;

    private void Raycasting() 
    {
        hitGround = Physics2D.Raycast (groundRay.transform.position, -Vector2.up);
        Debug.DrawRay (groundRay.transform.position, -Vector2.up * hitGround.distance, Color.red);
        //Debug.Log(hitGround.distance);

        hitGroundAlt = Physics2D.Raycast (groundRayAlt.transform.position, -Vector2.up);
        Debug.DrawRay (groundRayAlt.transform.position, -Vector2.up * hitGroundAlt.distance, Color.blue);

        hitObs = Physics2D.Raycast (obstacleRay.transform.position, Vector2.right * direction);
        Debug.DrawRay (obstacleRay.transform.position, Vector2.right * hitObs.distance * direction, Color.blue);

        //Debug.Log(hitObs.collider.gameObject.name);
        /*
        if (hitObs.collider != null) {
            Debug.Log ("Target name: " + hitObs.collider.name);
        }
        */
    
    }

    internal float DistanceFromGround() {
        return Math.Min(hitGround.distance, hitGroundAlt.distance);
    }
    internal bool Grounded() {
        return DistanceFromGround() < groundedValue && DistanceFromGround() != 0;
    }
}
