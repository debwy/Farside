using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    internal PlayerMain player;

    public int attackDamage = 5;
    public int maxHealth = 100;
    private int currentHealth = 100;
    public int shotDmgDivide = 2;

    public Transform attackPoint;
    public float attackRange = 1.15f;
    public LayerMask attackableLayers;

    public PlayerProjectile projectile;
    public Transform launcher;

    //[SerializeField] private float attackCooldown = 1f;
    private float lastAttack1 = -100f;
    private float lastAttack2 = -100f;
    //private float lastAttackRefill = -100f;
    [SerializeField] private float rangedCooldown = 2f;
    private float lastRanged = -100f;

    [SerializeField] private int rangedCount = 3;
    //[SerializeField] private int meleeCount = 3;
    [SerializeField] private float chainedAttackTime = 0.8f;
    private bool canShoot = true;
    private bool isDead = false;

    void Start()
    {
        player.healthbar.SetMaxHealth(maxHealth);
        player.healthbar.SetHealth(currentHealth);
    }

    internal void Shoot() {
        if (rangedCount < 3 && Time.time >= lastRanged + rangedCooldown) {
            lastRanged = Time.time;
            rangedCount = 3;
        }
        if (rangedCount > 0 && canShoot) {
            rangedCount--;
            Instantiate(projectile, launcher.position, launcher.rotation);
            player.ani.SetTrigger("Shoot");
        }
    }

    internal int GetShotAttackDmg() {
        return attackDamage/shotDmgDivide;
    }

    internal void MeleeAttack() {
        /*
        if (meleeCount < 3 && Time.time >= lastAttackRefill + attackCooldown) {
            lastAttackRefill = Time.time;
            meleeCount = 3;
        }
        if (meleeCount > 0) {
        */
        canShoot = false;
            if (Time.time - lastAttack2 < chainedAttackTime) {
                player.ani.SetTrigger("Attack3");
            } else if (Time.time - lastAttack1 < chainedAttackTime) {
                player.ani.SetTrigger("Attack2");
                lastAttack2 = Time.time;
            } else {
                player.ani.SetTrigger("Attack");
                lastAttack1 = Time.time;
            }
        /*
            meleeCount--;
        }
        */
    }

    private void MeleeCollect() {
        canShoot = true;
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
        if (!isDead) {
            isDead = true;
            Debug.Log("Player died");
            player.ani.SetBool("Died", true);

            player.body.velocity = Vector2.zero;
            player.body.angularVelocity = 0;

            player.enabled = false;

            //TODO sends player to game over screen
            StartCoroutine (SendingToGameOver());
        }
    }

    private IEnumerator SendingToGameOver() {
        //play sound: death
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
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

    internal void Load(GameData data) {
        this.attackDamage = data.attackDamage;
        this.maxHealth = data.maxHealth;
        this.currentHealth = data.currentHealth;
        this.shotDmgDivide = data.shotDmgDivide;
    }

    internal void Save(GameData data) {
        data.attackDamage = this.attackDamage;
        data.maxHealth = this.maxHealth;
        data.currentHealth = this.currentHealth;
        data.shotDmgDivide = this.shotDmgDivide;
    }
}
