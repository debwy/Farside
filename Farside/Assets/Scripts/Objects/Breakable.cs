using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IBreakable
{
    [SerializeField]public GameObject drop;
    [SerializeField] public Vector3 offset = new Vector3(0f, 0f, 0f);

    private Animator ani;
    private bool alreadyDropped = false;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void Break() {
        ani.SetTrigger("Break");
        if (!alreadyDropped && drop != null) {
            Debug.Log("bonk");
            alreadyDropped = true;
            Instantiate(drop, transform.position + offset, transform.rotation);
        }
    }
}
