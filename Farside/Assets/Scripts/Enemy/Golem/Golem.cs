using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Golem : MonoBehaviour, IEnemy
{
    public float speed = 0.5f;
    public int maxHealth = 200;
    internal int currentHealth;
    internal Rigidbody2D body;
    internal Animator ani;

    public int attackDamage = 30;
    public bool isFacingRight = true;
    private int faceRightInt;

    private bool ableToMove = true;
    internal bool died;
    internal bool isPlayerInAggroRange = false;
    internal Transform aggroTarget;

    public Healthbar healthbar;
    public LayerMask playerLayer;

    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject launcher;

    private bool canContact;
    public float contactCooldown = 0.5f;
    private float lastContact = -100;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        if (!isFacingRight) {
            faceRightInt = -1;
        } else {
            faceRightInt = 1;
        }
        currentHealth = maxHealth;
        died = false;
        healthbar.SetMaxHealth(maxHealth);
        initialLocation = initialLocationMarker.transform;
        canContact = true;
    }

    public GameObject obstacleRay;
    private RaycastHit2D hitObs;
    public float meleeRange = 2;

    [SerializeField]
    public GameObject initialLocationMarker;
    private Transform initialLocation;

    void Update()
    {

        hitObs = Physics2D.Raycast (obstacleRay.transform.position, faceRightInt * Vector2.right);
        Debug.DrawRay (obstacleRay.transform.position, faceRightInt * Vector2.right * hitObs.distance, Color.red);

        if (isPlayerInAggroRange && !died) {
            SetAggro();
            ani.SetBool("Idle", false);
            if (canContact) {
                NoTouchy();
            }
            AttackDecision();
        } else if (!died) {
            if (currentHealth != maxHealth) {
                ani.SetBool("Idle", true);
                ResetHealth();
            } else if (aggroTarget != null) {
                ani.SetBool("Idle", true);
                aggroTarget = null;
            } else if (Math.Abs(transform.position.x - initialLocation.transform.position.x) > 1f) {
                RunTowards(initialLocation);
            }
        }
    }

    //called upon seeing player in front via raycast (compare tag)
    internal void AttackDecision() {
        if (hitObs.collider != null && hitObs.collider.tag == "Player") {
            if (hitObs.distance <= meleeRange) {
                MeleeAttack();
            } else {
                RangedAttack();
            }
        } else {
            if (body != null) {
                RunTowards(aggroTarget);
            }
        }
    }

    //when player not in "sight" or when cooldown for abilities up
    private void RunTowards(Transform targetLocation) {
        if (ableToMove) {
            if (targetLocation.position.x - transform.position.x < -1f) {
                if (isFacingRight) {
                    Flip();
                }
            } else if (targetLocation.position.x - transform.position.x > 1f) {
                if (!isFacingRight) {
                    Flip();
                }
            }
            
            body.AddForce(new Vector2(speed * faceRightInt, 0f), ForceMode2D.Impulse);
        } else {
            body.velocity = Vector3.zero;
            body.angularVelocity = 0f;
        }
    }

    [SerializeField] private GameObject meleeEffect;
    [SerializeField] private GameObject enemyProjectile;
    private Vector3 offset = new Vector3(30f, -20f, 0f);
    private float lastMelee = -100;
    public float meleeCooldown = 6f;
    private float lastRanged = -100;
    public float rangedCooldown = 5f;

    //might want to add atk indicator in the future
    private void MeleeAttack() {
        if (Time.time < (lastMelee + meleeCooldown)) {
            RangedAttack();
        } else {
            StartCoroutine(MeleeAttackHelper());
        }

    }

    private IEnumerator MeleeAttackHelper() {
        ableToMove = false;
        lastMelee = Time.time;
        yield return new WaitForSeconds(1f);
        ani.SetTrigger("Melee");
    }

    //to be timed with melee animation
    public void SpawnMeleeAttack() {
        Instantiate(meleeEffect, transform.position, transform.rotation);
        ableToMove = true;
    }

    private void RangedAttack() {
        if (Time.time < (lastRanged + rangedCooldown)) {
            RunTowards(aggroTarget);
        } else {
            ableToMove = false;
            ani.SetTrigger("Ranged");
            lastRanged = Time.time;
        }
    }

    public void SpawnRangedAttack() {
        Instantiate(enemyProjectile, launcher.transform.position, launcher.transform.rotation);
        ableToMove = true;
    }

    private void SetAggro() {
        aggroTarget = GameObject.Find("Player").transform;
    }
    
    public void NotifyAggro(bool input) {
        isPlayerInAggroRange = input;
    }

    public void TakeDamage(int damage) {
        if (currentHealth > 0) {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
            ani.SetTrigger("Hit");
        }
        if(currentHealth <= 0) {
            Die();
        }
    }

    public void TakeRangedDamage(int damage) {
        //even if sniped, enemy auto heals quicker
        //does the same as takeDamage but no animation
        if (currentHealth > 0) {
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
        }
        if(currentHealth <= 0) {
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
            healthbar.SetHealth(currentHealth);
        }
    }

    private void Flip() {
        transform.Rotate(0f, 180f, 0f);
        healthbar.transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
        faceRightInt *= -1;
    }

    internal void ResetHealth() {
        currentHealth = maxHealth;
    }

    private void NoTouchy() {
        if (col.IsTouchingLayers(playerLayer) && Time.time >= (lastContact + contactCooldown)) {
            lastContact = Time.time;
            GameObject.Find("Player").GetComponent<PlayerMain>().TakeDamage(attackDamage/5);
        }
    }

    public void Death() {
        TakeDamage(maxHealth);
    }

    public bool IsDead() {
        return died;
    }

    public void Die() {
        died = true;
        canContact = false;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
        ani.SetBool("Died", true);
    }

    public void DespawnEnemy() {
        this.enabled = false;
        Destroy(gameObject);
    }

}
