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
        SlimeDeathEvent?.Invoke();
    }

    public void StartGolemDeathEvent(int id) {
        GolemDeathEvent?.Invoke(id);
    }

    public void StartBatDeathEvent() {
        BatDeathEvent?.Invoke();
    }

    public void StartChestOpenEvent() {
        ChestOpenEvent?.Invoke();
    }
}
