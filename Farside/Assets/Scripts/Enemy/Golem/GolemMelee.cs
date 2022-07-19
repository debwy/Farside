using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMelee : MonoBehaviour
{

    public int attackDamage = 10;
    public LayerMask playerLayer;
    public Collider2D col;

    private bool isDamaging = true;

    void Update()
    {
        if (isDamaging) {
            MeleeAttacking();
        }
    }

    private void MeleeAttacking() {
        if (col.IsTouchingLayers(playerLayer)) {
            GameObject.Find("Player").GetComponent<PlayerMain>().TakeDamage(attackDamage);
            NoMoreAttack();
        }
    }

    public void NoMoreAttack() {
        isDamaging = false;
    }

    public void MeleeEnd() {
        this.enabled = false;
        Destroy(gameObject);
    }

}
