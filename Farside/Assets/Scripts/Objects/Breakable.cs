using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IBreakable
{
    public void Break() {
        Debug.Log("Destroyed");
        this.enabled = false;
        Destroy(gameObject);
    }
}
