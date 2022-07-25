using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;

    void Start() {
        tutorial.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D hit) {
        if (hit.gameObject.CompareTag("Player")) {
            tutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D (Collider2D hit) {
        if (hit.gameObject.CompareTag("Player")) {
            tutorial.SetActive(false);
        }
    }
}
