using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    //[SerializeField]
    //internal PlayerMain player;

    public int speed = 10;
    internal Rigidbody2D body;
    internal Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        ani = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = transform.right * speed;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision) {
        //ani.SetTrigger("Hit");
        Destroy(gameObject);
    }
    */

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit != null) {
            if (hit.gameObject.CompareTag("Enemy")) {
                int dmg = GameObject.Find("Player").GetComponent<PlayerMain>().GetShotAttackDmg();
                hit.GetComponent<Enemy>().TakeDamage(dmg);
                Debug.Log("Enemy shot");
            }
            if (!hit.gameObject.CompareTag("Player") && !hit.gameObject.CompareTag("PlayerProjectile")) {
                Debug.Log(hit.name);
                Destroy(gameObject);
            }
        }
    }

}
