using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRanged : MonoBehaviour, IEnemyProjectile
{
    public int attackDamage = 10;
    public int parryDamage = 50;
    public int speed = 10;
    internal Rigidbody2D body;

    private bool isParried = false;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hit) {
        if (!isParried) {
            if (hit.gameObject.CompareTag("Player")) {
                hit.GetComponent<PlayerMain>().TakeDamage(attackDamage);
            }
            if (!hit.gameObject.CompareTag("Enemy") && !hit.gameObject.CompareTag("EnemyProjectile") && !hit.gameObject.CompareTag("RangeMarker")) {
                Destroy(gameObject);
            }
        } else {
            if (hit.gameObject.CompareTag("Enemy")) {
                hit.GetComponent<IEnemy>().TakeDamage(parryDamage);
            }
            if (!hit.gameObject.CompareTag("Player") && !hit.gameObject.CompareTag("PlayerProjectile") && !hit.gameObject.CompareTag("RangeMarker")) {
                Destroy(gameObject);
            }
        }
    }

    public void Flip() {
        transform.Rotate(0f, 180f, 0f);
    }

    public void Parry() {
        isParried = true;
        Flip();
    }
}
