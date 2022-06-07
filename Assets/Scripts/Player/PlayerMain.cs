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

    // Start is called before the first frame update
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.attacking) {
            ani.SetTrigger("Attack");
            combat.MeleeAttack();
        }
    }

    void FixedUpdate() {
        if (input.moveHorizontal > 0.1f || input.moveHorizontal < -0.1f) {
            movement.HorizontalMovement(input.moveHorizontal, input.isJumping);
        }
        if(input.DistanceFromGround() < 0.1f && input.moveVertical > 0.1f) {
            movement.VerticalMovement(input.moveVertical);
        }
    }

    public void TakeDamage(int attackDamage) {
        combat.TakeDamage(attackDamage);
    }
}
