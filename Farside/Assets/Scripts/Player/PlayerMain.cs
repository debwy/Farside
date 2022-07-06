using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField]
    internal PlayerInput input;
    [SerializeField]
    internal PlayerMovement movement;
    [SerializeField]
    internal PlayerCollision collision;
    [SerializeField]
    internal PlayerCombat combat;

    internal Animator ani;
    internal Rigidbody2D body;

    internal bool faceRight;
    internal bool enableMovement;
    internal bool enableActions;

    [SerializeField]
    internal Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        faceRight = true;
        enableMovement = true;
        enableActions = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableActions) {
            PlayerActions();
        }
    }

    private void PlayerActions() {
        if (input.attacking) {
            ani.SetTrigger("Attack");
            combat.MeleeAttack();
        }

        if (input.shooting) {
            ani.SetTrigger("Shoot");
            combat.Shoot();
        }

        if (input.dashing) {
            movement.TryDash();
        }
        movement.CheckDash(faceRight);
    }

    void FixedUpdate() {

        if (enableMovement) {
            PlayerMovement();
        }
    }

    private void PlayerMovement() {
        if(input.Grounded()) {
            ani.SetBool("Ground", true);
        } else {
            ani.SetBool("Ground", false);
        }

        ani.SetBool("Run", input.moveHorizontal != 0);

        if (input.moveHorizontal > 0.1f) {
            if (!faceRight) {
                Flip();
            }
            faceRight = true;
            movement.HorizontalMovement(input.moveHorizontal, input.isJumping);
        }
        if (input.moveHorizontal < -0.1f) {
            if (faceRight) {
                Flip();
            }
            faceRight = false;
            movement.HorizontalMovement(input.moveHorizontal, input.isJumping);
        }
        if(input.Grounded() && input.moveVertical > 0.1f) {
            ani.SetTrigger("Jump");
            movement.VerticalMovement(input.moveVertical);
        }
    }

    public void TakeDamage(int attackDamage) {
        combat.TakeDamage(attackDamage);
    }

    private void Flip() {
        transform.Rotate(0f, 180f, 0f);
        healthbar.transform.Rotate(0f, 180f, 0f);
    }

    public int GetShotAttackDmg() {
        return combat.GetShotAttackDmg();
    }

    public void Death() {
        combat.Instakill();
    }

    public void GameOver() {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
