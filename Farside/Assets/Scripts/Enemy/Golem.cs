using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour, IEnemy
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
    public LayerMask playerLayer;

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

    public GameObject obstacleRay;
    private RaycastHit2D hitObs;
    public float meleeValue = 0.3f;

    void Update()
    {
        
    }

    //called upon seeing player in front via raycast (compare tag)
    internal void AttackDecision() {
        if (hitObs.distance <= 0.3f) {
            MeleeAttack();
        } else {
            RangedAttack();
        }
    }

    internal void MeleeAttack() {

    }

    internal void RangedAttack() {
        
    }

    public void TakeDamage(int damage) {

    }

    public void Die() {

    }

    public bool IsDead() {
        return died;
    }

}
