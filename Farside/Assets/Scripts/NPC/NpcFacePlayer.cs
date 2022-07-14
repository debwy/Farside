using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcFacePlayer : MonoBehaviour
{

    public GameObject obstacleRay;
    private RaycastHit2D hitObs;

    [SerializeField]
    private bool isFacingRight = true;
    private int rightInt;
    private bool isAbleToFlip = true;
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
    
        //Debug.Log(hitObs.collider.name);

        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            isAbleToFlip = false;
        } else {
            isAbleToFlip = true;
        }

        if (isCheckingForPlayer) {
            if (hitObs.collider.tag != "Player" && isAbleToFlip) {
                Flip();
                isAbleToFlip = false;
                StartCoroutine(Wait(3f));
                isAbleToFlip = true;
            }
        }
    }

    public void Flip() {
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        rightInt *= -1;
    }

    //prevents npc from partying if player is colliding but not in sight
    public IEnumerator Wait(float waiting) {
        yield return new WaitForSeconds(waiting);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            isCheckingForPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            isCheckingForPlayer = false;
        }
    }

}
