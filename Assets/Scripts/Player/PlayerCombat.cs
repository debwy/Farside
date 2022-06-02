using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    internal PlayerMain player;

    public int attackDamage;
    public int totalHealth;
    private int currentHealth;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    void Start()
    {
        currentHealth = totalHealth;
    }

    internal void MeleeAttack() {
        player.ani.SetTrigger("Attack");

        //detects enemies in range of atk & collects them in an array
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damages enemies
        foreach(Collider2D enemy in hitEnemies) {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage) {
        
        if (currentHealth > 0) {
            currentHealth -= damage;

            //play hurt animation
            player.ani.SetTrigger("Hit");
        }

        if(currentHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        Debug.Log("Player died");
        player.ani.SetBool("Died", true);

        player.body.velocity = Vector2.zero;
        player.body.angularVelocity = 0;

        player.enabled = false;

        //TODO sends player to game over screen
    }
}
