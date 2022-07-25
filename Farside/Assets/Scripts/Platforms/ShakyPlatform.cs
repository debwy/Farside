using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyPlatform : MonoBehaviour
{
    [SerializeField] private Collider2D col;
    //[SerializeField] private Collider2D platform;
    //[SerializeField] private LayerMask thingsLayer;
    private Rigidbody2D body;
    private Animator ani;

    [SerializeField] private float respawnTime = 2f;
    [SerializeField] private float timeBeforeDrop = 1f;
    [SerializeField] private float dropSpeed = 0.2f;
    private bool activated = false;
    private float lastDrop;
    private float playerStandTime;
    private float initialPositionx;
    private float initialPositiony;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        activated = false;
        ani.SetBool("Dropping", false);
        initialPositionx = this.transform.position.x;
        initialPositiony = this.transform.position.y;
        playerStandTime = -1;
    }

    void FixedUpdate() {
        if (activated) {
            ani.SetBool("Dropping", true);
        }

        if (activated && Time.time >= playerStandTime + timeBeforeDrop) {
            transform.position = Vector3.MoveTowards(transform.position, transform.position-Vector3.up, dropSpeed);
            //body.bodyType = RigidbodyType2D.Dynamic;
        }
        if (activated && Time.time >= playerStandTime + timeBeforeDrop + respawnTime) {
            RespawnPlatform();
        }
    }

    void RespawnPlatform() {
        ani.SetBool("Dropping", false);
        body.bodyType = RigidbodyType2D.Static;
        transform.position = new Vector3(initialPositionx, initialPositiony, 0);
        activated = false;
        playerStandTime = -1;
    }

    private void OnCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.CompareTag("Player") || hit.gameObject.CompareTag("Enemy")) {
            activated = true;
            if (playerStandTime == -1) {
                playerStandTime = Time.time;
            }
        }
    }

}
