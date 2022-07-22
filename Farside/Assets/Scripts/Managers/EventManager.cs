using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance {get; private set;}

    private void Awake()
    {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public event Action SlimeDeathEvent;
    public event Action<int> GolemDeathEvent;
    public event Action BatDeathEvent;
    public event Action ChestOpenEvent;

    public void StartSlimeDeathEvent() {
        Debug.Log("Invoking Event");
        SlimeDeathEvent?.Invoke();
    }

    public void StartGolemDeathEvent(int id) {
        Debug.Log("Invoking Event");
        GolemDeathEvent?.Invoke(id);
    }

    public void StartBatDeathEvent() {
        Debug.Log("Invoking Event");
        BatDeathEvent?.Invoke();
    }

    public void StartChestOpenEvent() {
        Debug.Log("Invoking Event");
        ChestOpenEvent?.Invoke();
    }
}
