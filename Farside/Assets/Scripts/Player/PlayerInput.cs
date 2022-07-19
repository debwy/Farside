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
    internal bool isInteracting;

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
        isInteracting = Input.GetKeyDown(KeyCode.Space);
        
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
    public float groundedValue = 0.2f;
    public float groundedValueOnSlope = 0.4f;

    public GameObject slopeRay;
    private RaycastHit2D hitSlope;
    public GameObject slopeRayAlt;
    private RaycastHit2D hitSlopeAlt;

    private void Raycasting() 
    {
        hitGround = Physics2D.Raycast (groundRay.transform.position, -Vector2.up);
        Debug.DrawRay (groundRay.transform.position, -Vector2.up * hitGround.distance, Color.red);

        hitGroundAlt = Physics2D.Raycast (groundRayAlt.transform.position, -Vector2.up);
        Debug.DrawRay (groundRayAlt.transform.position, -Vector2.up * hitGroundAlt.distance, Color.blue);

        hitSlope = Physics2D.Raycast (slopeRay.transform.position, -Vector2.up);
        Debug.DrawRay (slopeRay.transform.position, -Vector2.up * hitSlope.distance, Color.blue);

        hitSlopeAlt = Physics2D.Raycast (slopeRayAlt.transform.position, -Vector2.up);
        Debug.DrawRay (slopeRayAlt.transform.position, -Vector2.up * hitSlopeAlt.distance, Color.red);
    }

    internal float DifferenceInDistance() {
        return hitSlope.distance - hitSlopeAlt.distance;
    }

    //used to determine whether movement speed is increased (uphill)
    internal bool SlopeCheck() {
        return DifferenceInDistance() > 0.1f && DifferenceInDistance() < 0.7f;
    }

    //used to determine whether alternate ground check can be used (any slope)
    private float absoluteDistance;
    internal bool SlopeGroundedCheck() {
        absoluteDistance = Math.Abs(DifferenceInDistance());
        return absoluteDistance > 0.1f && absoluteDistance < 0.7f;
    }

    internal float DistanceFromGround() {
        return Math.Min(hitGround.distance, hitGroundAlt.distance);
    }

    internal bool Grounded() {
        return (DistanceFromGround() < groundedValue && DistanceFromGround() != 0) || (SlopeGroundedCheck() && DistanceFromGround() < groundedValueOnSlope);
    }
}
