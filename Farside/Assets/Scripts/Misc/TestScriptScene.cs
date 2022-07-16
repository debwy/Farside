using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.CompareTag("Player")) {
            if (Loader.CurrentSceneIndex() == 1) {
                Loader.LoadScene(Loader.Scenes.Map1a);
            } else {
                Loader.LoadScene(Loader.Scenes.Map1);
            }
        }
    }
}