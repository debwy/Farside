using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int speed = 10;
    internal Rigidbody2D body;

    // Start is called before the first frame update
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
            if (hit.gameObject.CompareTag("Enemy")) {
                int dmg = GameObject.Find("Player").GetComponent<PlayerMain>().GetShotAttackDmg();
                hit.GetComponent<IEnemy>().TakeRangedDamage(dmg);
            }
            if (hit.gameObject.CompareTag("Enemy") || hit.gameObject.CompareTag("Ground") || hit.gameObject.CompareTag("EnemyProjectile") || hit.gameObject.CompareTag("Trap")) {
                Destroy(gameObject);
            }
        }
    }
}
