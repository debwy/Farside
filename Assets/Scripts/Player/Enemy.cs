using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;
    private Animator ani;

    public int attackDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        ani = GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        
        if (currentHealth > 0) {
            currentHealth -= damage;

            //play hurt animation
            ani.SetTrigger("Hit");
        }

        if(currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Enemy died");

        //play die animation
        ani.SetBool("Died", true);

        //disables enemy
        this.enabled = false;

        StartCoroutine(DespawnEnemy());
    }

    private IEnumerator DespawnEnemy() {
        yield return new WaitForSeconds(3);

        GetComponent<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider hit) {
        if (hit.gameObject.tag == "Player") {
            hit.GetComponent<PlayerMain>().TakeDamage(attackDamage);
            Debug.Log("Player hit");
        }
    }

}


