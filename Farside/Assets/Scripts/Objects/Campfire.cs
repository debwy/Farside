using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{

    [SerializeField] private int healingAmount = 5;
    [SerializeField] private float healIntervals = 0.5f;
    private bool healingPlayer = false;
    private float lastHeal = -100f;

    void Update() {
        if (healingPlayer && Time.time >= lastHeal + healIntervals) {
            lastHeal = Time.time;
            GameObject.Find("Player").GetComponent<PlayerMain>().Heal(healingAmount);
        }
    }

    public void OnTriggerEnter2D (Collider2D hit) {
        if (hit.CompareTag("Player")) {
            healingPlayer = true;
        }
    }

    public void OnTriggerExit2D (Collider2D hit) {
        if (hit.CompareTag("Player")) {
            healingPlayer = false;
        }
    }
}
