using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    private Animator ani;
    [SerializeField] GameObject transitionObject;

    void Awake() {
        ani = GetComponent<Animator>();
    }

    public void ExitSceneTransition() {
        transitionObject.SetActive(true);
        ani.SetTrigger("ExitScene");
    }

    public void FinishedTransitionIn() {
        transitionObject.SetActive(false);
    }
}
