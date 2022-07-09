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
    public int shotDmgDivide;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public PlayerProjectile projectile;
    public Transform launcher;

    void Start()
    {
        currentHealth = totalHealth;
        player.healthbar.SetMaxHealth(totalHealth);
        shotDmgDivide = 5;
    }

    internal void Shoot() {
        Instantiate(projectile, launcher.position, launcher.rotation);
    }

    internal int GetShotAttackDmg() {
        return attackDamage/shotDmgDivide;
    }

    internal void MeleeAttack() {

        //detects enemies in range of atk & collects them in an array
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damages enemies
        foreach(Collider2D enemy in hitEnemies) {
            enemy.GetComponent<IEnemy>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage) {
        
        if (currentHealth > 0) {
            currentHealth -= damage;

            player.healthbar.SetHealth(currentHealth);

            //play hurt animation
            player.ani.SetTrigger("Hurt");
        }

        if(currentHealth <= 0) {
            player.enableActions = false;
            player.enableMovement = false;
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
        StartCoroutine (SendingToGameOver());
    }

    private IEnumerator SendingToGameOver() {
        yield return new WaitForSeconds(1);
        player.GameOver();
    }

    internal void Instakill() {
        TakeDamage(totalHealth);
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        } else {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
