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

    void Start()
    {
        if (isFacingRight) {
            rightInt = 1;
        } else {
            rightInt = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hitObs = Physics2D.Raycast (obstacleRay.transform.position, -Vector2.up);
        Debug.DrawRay (obstacleRay.transform.position, rightInt * Vector2.right * hitObs.distance, Color.red);
    
        Debug.Log(hitObs.collider.name);

        if (DialogueManager.GetInstance().dialogueIsPlaying) {
            isAbleToFlip = false;
        } else {
            isAbleToFlip = true;
        }
    }

    public void Flip() {
        Debug.Log("Flipping");
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        rightInt *= -1;
    }

    //prevents npc from partying if player is colliding but not in sight
    private IEnumerator Wait() {
        yield return new WaitForSeconds(1);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (hitObs.collider.tag != "Player" && isAbleToFlip) {
                Flip();
                StartCoroutine(Wait());
            }
        }
    }

}
