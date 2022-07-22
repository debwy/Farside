using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slime : MonoBehaviour, IEnemy
{
    public int maxHealth = 100;
    internal int currentHealth;
    internal Rigidbody2D body;
    internal Animator ani;

    public int attackDamage = 10;
    public bool isInitiallyFacingRight = true;
    internal bool isFacingRight;
    internal int faceRightInt;
    public bool abilityToTakeRangedDamage = true;

    internal bool died;

    public Healthbar healthbar;
    public LayerMask playerLayer;

    [SerializeField]
    private Collider2D col;

    private bool canContact;
    public float contactCooldown = 0.5f;
    private float lastContact = -100;

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
        canContact = true;
    }

    //might shift this to specific enemy
    void Update()
    {
        if (canContact) {
            NoTouchy();
        }
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

    public void Heal(int healing) {
        if (!died && currentHealth < maxHealth) {
            if (currentHealth + healing <= maxHealth) {
                currentHealth += healing;
            } else {
                currentHealth = maxHealth;
            }
            healthbar.SetHealth(currentHealth);
        }
    }

    public void TakeRangedDamage(int damage) {
        if (abilityToTakeRangedDamage) {
            TakeDamage(damage);
        }
    }

    public void Death() {
        TakeDamage(maxHealth);
    }

    public void Die() {
        if (!died) {
        died = true;
        canContact = false;
        EventManager.instance.StartSlimeDeathEvent();

        body.velocity = Vector2.zero;
        body.angularVelocity = 0;

        //play die animation
        ani.SetBool("Died", true);
        }
    }

    private void DespawnEnemy() {
        //disables enemy
        this.enabled = false;
        Destroy(gameObject);
    }

    //might want to split this off later
    private void NoTouchy() {
        if (col.IsTouchingLayers(playerLayer) && Time.time >= (lastContact + contactCooldown)) {
            lastContact = Time.time;
            GameObject.Find("Player").GetComponent<PlayerMain>().TakeDamage(attackDamage);
        }
    }

    internal Rigidbody2D Body() {
        return body;
    }

    internal bool IsFacingRight() {
        return isFacingRight;
    }

    public bool IsDead() {
        return died;
    }

    public void NotifyAggro(bool input) {
        //do nothing
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

