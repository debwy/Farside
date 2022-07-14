using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.CompareTag("Player")) {
            Loader.Load(Loader.Scene.Map1a);
        }
    }
}