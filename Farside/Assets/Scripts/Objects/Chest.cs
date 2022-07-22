using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [SerializeField] private int chestId;
    [SerializeField] private bool hasAssociatedGolem;
    private bool openable;
    internal Animator ani;
    

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid() {
        id = System.Guid.NewGuid().ToString();
    }

    private bool isOpened = false;

    void Awake() {
        ani = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        openable = !hasAssociatedGolem;
        EventManager.instance.GolemDeathEvent += OpenChest;
    }

    void OnDisable()
    {
        EventManager.instance.GolemDeathEvent -= OpenChest;
    }

    public void OpenChest(int golemId) {
        if(!isOpened && golemId == chestId) {
            openable = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D hit) {
        if (hit.CompareTag("Player") && openable && !isOpened) {
            ani.SetBool("Open", true);
            isOpened = true;
            EventManager.instance.StartChestOpenEvent();
        }
    }

    public void LoadData(GameData data) {
        data.chestsOpened.TryGetValue(id, out isOpened);
        if (isOpened) {
            openable = false;
            ani.SetBool("Open", true);
        }
    }

    public void SaveData(GameData data) {
        if (data.chestsOpened.ContainsKey(id)) {
            data.chestsOpened.Remove(id);
        }
        data.chestsOpened.Add(id, isOpened);
    }
}
