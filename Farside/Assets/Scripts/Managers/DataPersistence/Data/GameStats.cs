using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour, IDataPersistence
{
    private int slimeDeaths = 0;
    private int golemDeaths = 0;
    private int batDeaths = 0;
    private int chestOpenCount = 0;

    void Start() {
        EventManager.instance.SlimeDeathEvent += AddSlimeDeathCount;
        EventManager.instance.GolemDeathEvent += AddGolemDeathCount;
        EventManager.instance.BatDeathEvent += AddBatDeathCount;
        EventManager.instance.ChestOpenEvent += AddChestOpenCount;
    }

    void OnDisable() {
        EventManager.instance.SlimeDeathEvent -= AddSlimeDeathCount;
        EventManager.instance.GolemDeathEvent -= AddGolemDeathCount;
        EventManager.instance.BatDeathEvent -= AddBatDeathCount;
        EventManager.instance.ChestOpenEvent -= AddChestOpenCount;
    }

    public void LoadData(GameData data) {
        this.slimeDeaths = data.slimeDeaths;
        this.golemDeaths = data.golemDeaths;
        this.batDeaths = data.batDeaths;
        this.chestOpenCount = data.chestOpenCount;
        //Debug.Log("Loading game stats");
        //Debug.Log(this.slimeDeaths);
    }

    public void SaveData(GameData data) {
        Debug.Log("Saving game stats");
        Debug.Log(this.slimeDeaths);
        data.slimeDeaths = this.slimeDeaths;
        data.golemDeaths = this.golemDeaths;
        data.batDeaths = this.batDeaths;
        data.chestOpenCount = this.chestOpenCount;
        //Debug.Log("Finished Saving game stats");
        //Debug.Log(this.slimeDeaths);
    }

    public int GetEnemyDeathCount() {
        return slimeDeaths + golemDeaths + batDeaths;
    }

    public void AddSlimeDeathCount() {
        Debug.Log("Slime count updated");
        slimeDeaths++;
        Debug.Log(slimeDeaths);
    }

    public void AddGolemDeathCount(int golemId) {
        golemDeaths++;
    }

    public void AddBatDeathCount() {
        batDeaths++;
    }

    public void AddChestOpenCount() {
        chestOpenCount++;
    }

}
