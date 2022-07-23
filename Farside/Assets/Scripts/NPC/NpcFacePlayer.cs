using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFacePlayer : MonoBehaviour
{

    public GameObject obstacleRay;
    private RaycastHit2D hitObs;
    public GameObject obstacleRayAlt;
    private RaycastHit2D hitObsAlt;

    [SerializeField]
    private bool isFacingRight = true;
    private int rightInt;
    private bool isCheckingForPlayer = false;

    void Start()
    {
        if (isFacingRight) {
            rightInt = 1;
        } else {
            rightInt = -1;
        }
    }

    void Update()
    {
        hitObs = Physics2D.Raycast (obstacleRay.transform.position, rightInt * Vector2.right);
        Debug.DrawRay (obstacleRay.transform.position, rightInt * Vector2.right * hitObs.distance, Color.red);

        hitObsAlt = Physics2D.Raycast (obstacleRayAlt.transform.position, -rightInt * Vector2.right);
        Debug.DrawRay (obstacleRayAlt.transform.position, rightInt * Vector2.left * hitObsAlt.distance, Color.red);

        if (isCheckingForPlayer) {
            if (hitObs.collider != null && !hitObs.collider.CompareTag("Player")) {
                if(hitObsAlt.collider != null && hitObsAlt.collider.CompareTag("Player")) {
                    Flip();
                }
            }
        }
    }

    public void Flip() {
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        rightInt *= -1;
    }

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit != null && hit.CompareTag("Player")) {
            isCheckingForPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D hit) {
        if (hit != null && hit.CompareTag("Player")) {
            isCheckingForPlayer = false;
        }
    }

}
