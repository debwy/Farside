using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeProjectile : MonoBehaviour, IEnemyProjectile
{

    public int attackDamage = 10;
    public int speed = 10;
    internal Rigidbody2D body;

    private Transform player;
    private Vector2 directionOfTravel;

    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        directionOfTravel = (player.position - transform.position).normalized * speed;
    }

    void FixedUpdate()
    {
        body.velocity = new Vector2 (directionOfTravel.x, directionOfTravel.y);
    }

    void OnTriggerEnter2D(Collider2D hit) {
            if (hit.gameObject.CompareTag("Player")) {
                hit.GetComponent<PlayerMain>().TakeDamage(attackDamage);
            }
            if (!hit.gameObject.CompareTag("Enemy") && !hit.gameObject.CompareTag("EnemyProjectile") && !hit.gameObject.CompareTag("RangeMarker")) {
                Destroy(gameObject);
            }
    }

    public void Parry(){
        //do nothing
    }
}
