using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bat : MonoBehaviour, IEnemy
{

    private Transform player;

    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator ani;
    [SerializeField] private Healthbar healthbar;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private int moveSpeed = 1;
    [SerializeField] private int maxHealth = 60;
    [SerializeField] private float aggroDistance = 20f;
    [SerializeField] private float attackDistance = 5f;
    private int faceRightInt = 1;
    private int currentHealth;
    private bool isDead = false;
    private Transform attackPoint;
    [SerializeField] private float movementDamping = 0.3f;

    [SerializeField] private GameObject initialLocationMarker;
    private Transform initialLocation;
    private Transform aggroTarget;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        if (!isFacingRight) {
            faceRightInt = -1;
        } else {
            faceRightInt = 1;
        }
        currentHealth = maxHealth;
        isDead = false;
        healthbar.SetMaxHealth(maxHealth);
        initialLocation = initialLocationMarker.transform;
    }

    void Update() {
        player = GameObject.Find("Player").transform;

        if (CheckingDistanceFromPlayer() < aggroDistance) {
            ani.SetBool("Idle", true);
            if (Vector3.Distance(transform.position, initialLocation.position) > 0.5f) {
                FlyTowards(initialLocation);
            }
        } else {
            if (CheckingDistanceFromPlayer() < attackDistance) {
                Attack();
            } else {
                FlyTowards(player);
            }
        }
    }

    private void FlyTowards(Transform location) {
        //transform.position = Vector3.SmoothDamp(transform.position, location.position, moveSpeed, movementDamping);
    }

    private void Attack() {
        SetAggro(player);
        //Wait(0.5f);
    }

    private void Wait(float timing) {
        StartCoroutine(WaitHelper(timing));
    }

    private IEnumerator WaitHelper(float timing) {
        yield return new WaitForSeconds(2f);
    }

    private void SetAggro(Transform attackPoint) {
        aggroTarget = attackPoint;
    }

    private float CheckingDistanceFromPlayer() {
        return Vector3.Distance(player.position, transform.position);
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

    //ranged attacks are more effective against bats (instead of 1/2, deals dmg = full player atk)
    public void TakeRangedDamage(int damage) {
        TakeDamage(damage * 2);
    }

    public void Death() {
        TakeDamage(maxHealth);
    }

    public void Die() {
        isDead = true;
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
        ani.SetBool("Died", true);
    }

    public void DespawnEnemy() {
        this.enabled = false;
        Destroy(gameObject);
    }

    public bool IsDead() {
        return isDead;
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
}
