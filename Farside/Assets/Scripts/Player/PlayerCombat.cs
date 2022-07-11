using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    internal PlayerMain player;

    public int attackDamage;
    public int maxHealth;
    private int currentHealth;
    public int shotDmgDivide;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask attackableLayers;

    public PlayerProjectile projectile;
    public Transform launcher;

    void Start()
    {
        currentHealth = maxHealth;
        player.healthbar.SetMaxHealth(maxHealth);
        shotDmgDivide = 5;
    }

    internal void Shoot() {
        Instantiate(projectile, launcher.position, launcher.rotation);
    }

    internal int GetShotAttackDmg() {
        return attackDamage/shotDmgDivide;
    }

    internal void MeleeAttack() {
        //detects items in range of atk & collects them in an array
        Collider2D[] hitThings = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackableLayers);
        //calls specific methods for all
        foreach(Collider2D item in hitThings) {
            if (item.CompareTag("Enemy")) {
                item.GetComponent<IEnemy>().TakeDamage(attackDamage);
            } else if (item.CompareTag("Breakable")) {
                item.GetComponent<IBreakable>().Break();
            } else if (item.CompareTag("Interactable")) {
                item.GetComponent<IInteractable>().InteractViaAttack();
            } else if (item.CompareTag("EnemyProjectile")) {
                item.GetComponent<IEnemyProjectile>().Parry();
            }
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

    public void Heal(int healing) {
        if (currentHealth < maxHealth) {
            if (currentHealth + healing <= maxHealth) {
                currentHealth += healing;
            } else {
                currentHealth = maxHealth;
            }
            player.healthbar.SetHealth(currentHealth);
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
        TakeDamage(maxHealth);
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        } else {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
