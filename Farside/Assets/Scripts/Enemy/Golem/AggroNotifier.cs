using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroNotifier : MonoBehaviour
{

    [SerializeField]
    public GameObject enemy;

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.tag == "Player" && enemy != null) {
            Debug.Log("Enemy aggro'd");
            enemy.GetComponent<IEnemy>().NotifyAggro(true);
        }
    }

    void OnTriggerExit2D(Collider2D hit) {
        if (hit.tag == "Player" && enemy != null) {
            enemy.GetComponent<IEnemy>().NotifyAggro(false);
        }
    }
}
