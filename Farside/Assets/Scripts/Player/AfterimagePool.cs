using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterimagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;

    private Queue<GameObject> avail = new Queue<GameObject>();

    //singleton
    public static AfterimagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    //creates more game objects for the pool
    private void GrowPool()
    {
        for (int i = 0; i < 10; i++) {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    //to be called from other scripts via singleton
    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        avail.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (avail.Count == 0) {
            GrowPool();
        }

        var instance = avail.Dequeue();
        instance.SetActive(true);
        return instance;
    }

}
