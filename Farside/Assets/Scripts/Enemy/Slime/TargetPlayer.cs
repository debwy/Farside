using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{

    [SerializeField] private Slime enemy;
    [SerializeField] private float shotCooldown = 1f;
    private float lastShot = -100f;
    [SerializeField] private SlimeProjectile projectile;
    [SerializeField] private float aggroRange = 15f;
    private Rigidbody2D body;
    private Transform player;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        player = GameObject.Find("Player").transform;
        if (!enemy.died && Vector3.Distance(transform.position, player.position) < aggroRange) {
            if (Time.time >= lastShot + shotCooldown) {
                lastShot = Time.time;
                Instantiate(projectile, transform.position, Quaternion.identity);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
