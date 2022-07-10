using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRanged : MonoBehaviour, IBreakable
{
    public int attackDamage = 10;
    public int speed = 10;
    internal Rigidbody2D body;

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
        if (hit != null) {
            if (hit.gameObject.CompareTag("Player")) {
                hit.GetComponent<PlayerMain>().TakeDamage(attackDamage);
            }
            if (!hit.gameObject.CompareTag("Enemy") && !hit.gameObject.CompareTag("EnemyProjectile") && !hit.gameObject.CompareTag("RangeMarker")) {
                Break();
            }
        }
    }

    public void Break() {
        Destroy(gameObject);
    }
}
