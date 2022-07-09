using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstakillTrap : MonoBehaviour
{

    private Rigidbody2D body;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            ani.SetTrigger("Trigger");
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerMain>().Death();

        } else if (collision.tag == "Enemy") {
            ani.SetTrigger("Trigger");
            GameObject enemy = collision.gameObject;
            enemy.GetComponent<IEnemy>().Death();
        }
    }
}
