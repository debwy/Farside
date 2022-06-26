using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    internal PlayerMain player;

    public float moveSpeed = 1;
    public float jumpForce = 1;

    public float dashTime = 0.05f;
    public float dashSpeed = 50;
    public float distanceBetweenImages = 0.1f;
    public float dashCooldown = 2.5f;

    private bool isDashing;
    private float dashTimeLeft;
    private float lastImageXPos;
    private float lastDash = -100;

    //might want to use raycast/collision(?), either way, TODO (or remove)
    private bool isTouchingWall = false;

    void Start()
    {
    }

    internal void TryDash() {
        if (Time.time >= (lastDash + dashCooldown)) {
            Dash();
        }
    }

    private void Dash() {
        isDashing = true;
        player.ani.SetBool("Dash", true);
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        AfterimagePool.Instance.GetFromPool();
        lastImageXPos = transform.position.x;
    }

    internal void CheckDash(bool faceRight) {
        if (isDashing) {
            if (dashTimeLeft > 0 && !isTouchingWall) {

                float sign;
                if (faceRight) {
                    sign = 1f;
                } else {
                    sign = -1f;
                }

                player.enableMovement = false;
                player.body.velocity = new Vector2(dashSpeed * sign, 0);
                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImageXPos) > distanceBetweenImages) {
                    lastImageXPos = transform.position.x;
                }

            } else {
                isDashing = false;
                player.ani.SetBool("Dash", false);
                player.enableMovement = true;
            }
        }
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
