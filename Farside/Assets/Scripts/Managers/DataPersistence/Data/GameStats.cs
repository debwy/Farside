using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour, IDataPersistence
{

    public int slimeDeaths;
    public int golemDeaths;
    public int batDeaths;
    public int chestOpenCount;

    void Start() {
        EventManager.instance.SlimeDeathEvent += AddSlimeDeathCount;
        EventManager.instance.GolemDeathEvent += AddGolemDeathCount;
        EventManager.instance.BatDeathEvent += AddBatDeathCount;
        EventManager.instance.ChestOpenEvent += AddChestOpenCount;
        slimeDeaths = 0;
        golemDeaths = 0;
        batDeaths = 0;
        chestOpenCount = 0;
    }

    void OnDisable() {
        EventManager.instance.SlimeDeathEvent -= AddSlimeDeathCount;
        EventManager.instance.GolemDeathEvent -= AddGolemDeathCount;
        EventManager.instance.BatDeathEvent -= AddBatDeathCount;
        EventManager.instance.ChestOpenEvent -= AddChestOpenCount;
    }

    public void LoadData(GameData data) {
        Debug.Log("Loading game stats");
        Debug.Log(slimeDeaths);
        this.slimeDeaths = data.slimeDeaths;
        this.golemDeaths = data.golemDeaths;
        this.batDeaths = data.batDeaths;
        this.chestOpenCount = data.chestOpenCount;
    }

    public void SaveData(GameData data) {
        Debug.Log("Saving game stats");
        Debug.Log(slimeDeaths);
        data.slimeDeaths = this.slimeDeaths;
        data.golemDeaths = this.golemDeaths;
        data.batDeaths = this.batDeaths;
        data.chestOpenCount = this.chestOpenCount;
    }

    public int GetEnemyDeathCount() {
        return slimeDeaths + golemDeaths + batDeaths;
    }

    public void AddSlimeDeathCount() {
        slimeDeaths++;
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
