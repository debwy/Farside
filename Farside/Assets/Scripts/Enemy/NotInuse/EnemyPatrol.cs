using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]
    Slime enemy;

    public GameObject groundRay;
    private RaycastHit2D hitGround;
    public float groundedValue = 0.5f;

    public GameObject wallRay;
    private RaycastHit2D hitWall;
    public float wallValue = 0.7f;

    public float speed = 0.5f;

    internal bool isPatrolling;
    private bool isWallOrEnemy;

    void Start()
    {
        isPatrolling = true;
    }

    void FixedUpdate()
    {
        Raycasting();

        if (isPatrolling && !enemy.IsDead()) {
            Patrol();
        }

    }

    private void Raycasting() {
        hitGround = Physics2D.Raycast (groundRay.transform.position, -Vector2.up);
        Debug.DrawRay (groundRay.transform.position, -Vector2.up * hitGround.distance, Color.red);

        if (enemy.IsFacingRight()) {
            hitWall = Physics2D.Raycast (wallRay.transform.position, Vector2.right);
            Debug.DrawRay (wallRay.transform.position, Vector2.right * hitWall.distance, Color.blue);
        } else {
            hitWall = Physics2D.Raycast (wallRay.transform.position, -Vector2.right);
            Debug.DrawRay (wallRay.transform.position, Vector2.left * hitWall.distance, Color.blue);
        }

        //Debug.Log(hitWall.distance);
        //Debug.Log(hitGround.distance);
        /*
        if (hitWall.collider != null) {
            Debug.Log ("Target name: " + hitWall.collider.name);
        }
        */
    }

    private void Patrol() {
        if (HittingWall() || !Grounded()) {
            PatrolFlip();
        } else if (enemy.Body() != null) {
            enemy.Body().AddForce(new Vector2(speed * enemy.faceRightInt, 0f), ForceMode2D.Impulse);
            //enemy.body.velocity = new Vector2(speed, enemy.body.velocity.y);
            //(moveHorizontal * moveSpeed/3, 0f)
        }
    }

    internal bool Grounded() {
        return DistanceFromGround() < groundedValue && DistanceFromGround() != 0;
        //groundChecker.IsTouchingLayers(groundLayers);
    }

    internal float DistanceFromGround() {
        return hitGround.distance;
    }

    internal bool HittingWall() {
        //Debug.Log(hitWall.distance);

        return hitWall.distance < wallValue && hitWall.distance > 0 && isWallOrEnemy;
        //bodyCollider.IsTouchingLayers(groundLayers);
    }

    internal float DistanceFromWall() {
        return hitWall.distance;
    }

    internal void PatrolFlip() {
        isPatrolling = false;
        enemy.Flip();
        //Debug.Log("Flipping");
        StartCoroutine(SetIsPatrolling());
    }

    private IEnumerator SetIsPatrolling() {
        yield return new WaitForSeconds(1);
        isPatrolling = true;
    }
}
