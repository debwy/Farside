using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    public int maxHealth = 100;
    internal int currentHealth;
    internal Rigidbody2D body;
    internal Animator ani;

    public int attackDamage = 10;
    public bool isInitiallyFacingRight = true;
    internal bool isFacingRight;
    internal int faceRightInt;

    internal bool died;

    public Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        isFacingRight = isInitiallyFacingRight;
        if (!isFacingRight) {
            faceRightInt = -1;
        } else {
            faceRightInt = 1;
        }
        currentHealth = maxHealth;
        died = false;
        healthbar.SetMaxHealth(maxHealth);
    }

    public void Flip() {
        transform.Rotate(0f, 180f, 0f);
        healthbar.transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        faceRightInt *= -1;
    }

    public void TakeDamage(int damage) {
        
        if (currentHealth > 0) {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);

            //play hurt animation
            ani.SetTrigger("Hit");
        }

        if(currentHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("Enemy died");

        died = true;

        body.velocity = Vector2.zero;
        body.angularVelocity = 0;

        //play die animation
        ani.SetBool("Died", true);

        //disables enemy
        this.enabled = false;

        StartCoroutine(DespawnEnemy());
    }

    private IEnumerator DespawnEnemy() {
        yield return new WaitForSeconds(2);

        //GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    /*
    void OnTriggerEnter2D(Collider2D hit) {
        if (hit != null) {
            if (hit.gameObject.CompareTag("Player")) {
                hit.GetComponent<PlayerMain>().TakeDamage(attackDamage);
                Debug.Log("Player touch");
            }
        }
    }
    */

}
