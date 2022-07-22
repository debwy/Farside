using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bat : MonoBehaviour, IEnemy
{

    private Transform player;
    private Rigidbody2D body;
    private Animator ani;
    [SerializeField] private Collider2D col;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private int maxHealth = 60;
    [SerializeField] private float aggroDistance = 15f;
    [SerializeField] private float attackDistance = 5f;
    private int currentHealth;
    private bool isDead = false;
    private Transform attackPoint;
    [SerializeField] private float normalDamping = 0.2f; //smaller value reaches target faster
    [SerializeField] private float normalSpeed = 5f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private GameObject initialLocationMarker;
    [SerializeField] private GameObject attackIndicator;
    private Transform initialLocation;
    private Vector3 aggroTarget;

    [SerializeField] private float attackCooldown = 3f;
    private float lastAttack = -100f;
    private bool canMove = true;
    private bool isHitByRanged = false;
    private bool isDiving = false;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        currentHealth = maxHealth;
        isDead = false;
        healthbar.SetMaxHealth(maxHealth);
        initialLocation = initialLocationMarker.transform;
    }

    void FixedUpdate() {
        player = GameObject.Find("Player").transform;

        if (!isDead) {
        if (isDiving && Vector3.Distance(transform.position, aggroTarget) > 0.3f) {
            DiveTowards(aggroTarget);
        } else {
            if (isDiving) {
                isDiving = false;
                Instantiate(attackIndicator, aggroTarget, transform.rotation);
                ani.SetTrigger("Attack");
            }

        if (CheckingDistanceFromPlayer() < aggroDistance || isHitByRanged) {
            if (CheckingDistanceFromPlayer() < attackDistance && CheckingDistanceFromPlayer() > 0.1f) {
                Attack();
            } else {
                FlyTowards(player.position);
            }
        } else {
            if (Vector3.Distance(transform.position, initialLocation.position) > 0.5f) {
                FlyTowards(initialLocation.position);
            } else if (currentHealth != maxHealth) {
                ResetHealth();
            }
        }
        }
        }
    }

    private void FlyTowards(Vector3 location) {
        if (canMove && ((location.x < transform.position.x && isFacingRight) || (location.x > transform.position.x && !isFacingRight))) {
            Flip();
        }
        if (canMove) {
            transform.position = Vector3.MoveTowards(transform.position, location, normalSpeed * Time.deltaTime);
            //body.MovePosition(transform.position + ((location - transform.position) * normalSpeed) * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, normalSpeed);
        }
    }

    private void DiveTowards(Vector3 location) {
        if (canMove && ((location.x < transform.position.x && isFacingRight) || (location.x > transform.position.x && !isFacingRight))) {
            Flip();
        }
        //while (Vector3.Distance(transform.position, location) > 1f) {
            //transform.position = Vector3.MoveTowards(transform.position, location, normalSpeed * Time.deltaTime);
            //body.MovePosition(transform.position + ((location - transform.position) * normalSpeed) * Time.deltaTime);
            //transform.position = Vector2.MoveTowards(transform.position, location, divingSpeed);
            transform.position = Vector3.SmoothDamp(transform.position, location, ref velocity, normalDamping);
        //}
    }

    private void Attack() {
        if (Time.time >= lastAttack + attackCooldown) {
            isHitByRanged = false;
            lastAttack = Time.time;
            SetAggro(player);
            Debug.Log("Atk0");
            StartCoroutine(AttackPart2());
        }
    }

    private IEnumerator AttackPart2() {
        Debug.Log("Atk1");
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Atk2");
        canMove = true;
        isDiving = true;
        DiveTowards(aggroTarget);
    }

    private void SetAggro(Transform attackPoint) {
        aggroTarget = attackPoint.position;
    }

    private float CheckingDistanceFromPlayer() {
        return Vector3.Distance(player.position, transform.position);
    }

    private float lastHit = -100f;
    [SerializeField] private float hitCooldown = 0.5f;
    private float lastHitAni = -100f;
    [SerializeField] private float hitAniCooldown = 2.5f;

    public void TakeDamage(int damage) {
        if (currentHealth > 0 && Time.time >= lastHit + hitCooldown) {
            lastHit = Time.time;
            currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
            if (Time.time >= lastHit + hitAniCooldown) {
                lastHitAni = Time.time;
                ani.SetTrigger("Hit");
            }
        }
        if(currentHealth <= 0) {
            Die();
        }
    }

    private void Flip() {
        transform.Rotate(0f, 180f, 0f);
        healthbar.transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }

    //ranged attacks are more effective against bats (instead of 1/2, deals dmg = full player atk)
    //however, ranged attacks from out of range aggros the bat
    public void TakeRangedDamage(int damage) {
        if (CheckingDistanceFromPlayer() > aggroDistance) {
            isHitByRanged = true;
        }
        TakeDamage(damage * 2);
    }

    public void Death() {
        TakeDamage(maxHealth);
    }

    public void Die() {
        if (!isDead) {
            isDead = true;
            EventManager.instance.StartBatDeathEvent();
            if (body.bodyType == RigidbodyType2D.Kinematic) {
                body.velocity = Vector2.zero;
                body.angularVelocity = 0;
            }
            ani.SetBool("Died", true);
            col.isTrigger = false;
            body.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void DespawnEnemy() {
        canMove = false;
        body.bodyType = RigidbodyType2D.Static;
        col.isTrigger = true;
        StartCoroutine(DespawnHelper());
    }

    private IEnumerator DespawnHelper() {
        yield return new WaitForSeconds(1f);
        this.enabled = false;
        Destroy(gameObject);
    }

    public bool IsDead() {
        return isDead;
    }

    private void ResetHealth() {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);
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

    public void NotifyAggro(bool input) {
        //aggro does not use collider
    }

    private void OnCollisionEnter2D(Collision2D hit) {
        if(hit.gameObject.CompareTag("Ground")) {
            if (isDead) {
                DespawnEnemy();
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
    }
}
