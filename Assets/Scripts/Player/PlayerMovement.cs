using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    internal PlayerMain player;

    public float moveSpeed = 1;
    public float jumpForce = 1;

    void Start()
    {

    }

    internal void HorizontalMovement(float moveHorizontal, bool isJumping) {
        if (isJumping) {
            player.body.AddForce(new Vector2(moveHorizontal * moveSpeed/3, 0f), ForceMode2D.Impulse);
         } else {
            player.body.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
         }
    }

    internal void VerticalMovement(float moveVertical) {
        player.body.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
    }
}
