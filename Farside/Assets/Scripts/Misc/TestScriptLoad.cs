using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptLoad : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D hit) {
        DataPersistenceManager.instance.LoadGame();
    }
}
