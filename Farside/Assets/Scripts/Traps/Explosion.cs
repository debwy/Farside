using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public int attackDamage = 30;
    public LayerMask playerLayer;
    public Collider2D col;

    private bool isExploding = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (isExploding) {
            Explode();
        }
    }

    private void Explode() {
        if (col.IsTouchingLayers(playerLayer)) {
            GameObject.Find("Player").GetComponent<PlayerMain>().TakeDamage(attackDamage);
            NoMoreExplode();
        }
    }

    public void NoMoreExplode() {
        isExploding = false;
    }

    public void Die() {
        this.enabled = false;
        Destroy(gameObject);
    }
}
