using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour, IInteractable
{
    [SerializeField]
    public int healAmount = 20;

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.CompareTag("Player") || hit.CompareTag("Enemy")) {
            Heal(hit);
            Destroy(gameObject);
        }
    }

    private void Heal(Collider2D hit) {
        if (hit.CompareTag("Player")) {
            hit.GetComponent<PlayerMain>().Heal(healAmount);
        } else {
            hit.GetComponent<IEnemy>().Heal(healAmount);
        }
    }

    public void InteractViaAttack() {
        Destroy(gameObject);
    }

    public void InteractViaButton() {
        //does nothing, will disappear upon contact
    }
}
